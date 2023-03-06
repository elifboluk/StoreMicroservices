using FreeProduct.Services.Order.Application.DTOs;
using FreeProduct.Services.Order.Application.Queries;
using FreeProduct.Services.Order.Infrastructure;
using FreeProduct.Shared.DTOs;
using MediatR;
using System.Linq;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FreeProduct.Services.Order.Application.Mapping;

namespace FreeProduct.Services.Order.Application.Handlers
{
    internal class GetOrdersByUserIdQueryHandler : IRequestHandler<GetOrdersByUserIdQuery, Response<List<OrderDto>>>
    {
        private readonly OrderDbContext _context;

        public GetOrdersByUserIdQueryHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<Response<List<OrderDto>>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
        {
            var orders = await _context.Orders.Include(x => x.OrderItems).Where(x => x.BuyerId == request.UserId).ToListAsync(); // Get OrderItems => UserId

            if (!orders.Any()) 
            {
                return Response<List<OrderDto>>.Success(new List<OrderDto>(), 200);
            }

            var ordersDto = ObjectMapper.Mapper.Map<List<OrderDto>>(orders); // Object mapper'dan gelen değerlerle mapleme yapılacak. 

            return Response<List<OrderDto>>.Success(ordersDto, 200);
        }
    }
}
