using FreeProduct.Services.Order.Application.DTOs;
using FreeProduct.Shared.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeProduct.Services.Order.Application.Commands
{
    /// <summary>
    ///  Veritabanına oluşturulacak olan yeni siparişler için. 
    ///  Veritabanına kaydedildikten sonra dto'dan oluşan siparişin id'sini dönüyoruz.
    /// </summary>
    public class CreateOrderCommand : IRequest<Response<CreatedOrderDto>>
    {
        public string BuyerId { get; set; } // Satın alanın Id'si.

        public List<OrderItemDto> OrderItems { get; set; } // Order item, kullanıcı sepeti, hangi ürünleri aldığının bilgisi. 

        public AddressDto Address { get; set; } // Adres bilgileri.
    }
}
