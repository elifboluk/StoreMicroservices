using FreeProduct.Services.Order.Application.Commands;
using FreeProduct.Services.Order.Application.DTOs;
using FreeProduct.Services.Order.Domain.OrderAggregate;
using FreeProduct.Services.Order.Infrastructure;
using FreeProduct.Shared.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeProduct.Services.Order.Application.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response<CreatedOrderDto>>
    {
        private readonly OrderDbContext _context;

        public CreateOrderCommandHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<Response<CreatedOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var newAddress = new Address(request.Address.Province, request.Address.District, request.Address.Street, request.Address.ZipCode, request.Address.Line); // Adres bilgilerini al.

            Domain.OrderAggregate.Order newOrder = new Domain.OrderAggregate.Order(request.BuyerId, newAddress); // Order varsa adresi veritabanına ekle. Order üzerinden ekleme işlemleri yaptığımız için Order'ı da bilgileriyle alıyoruz. 

            request.OrderItems.ForEach(x =>
            {
                newOrder.AddOrderItem(x.ProductId, x.ProductName, x.Price, x.PictureUrl);
            });

            await _context.Orders.AddAsync(newOrder); // Veritabanına ekle. 

            await _context.SaveChangesAsync(); // KAYDET. 

            return Response<CreatedOrderDto>.Success(new CreatedOrderDto { OrderId = newOrder.Id }, 200);
        }
    }
}
