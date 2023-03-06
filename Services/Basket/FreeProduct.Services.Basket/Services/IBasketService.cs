using FreeProduct.Services.Basket.DTOs;
using FreeProduct.Shared.DTOs;

namespace FreeProduct.Services.Basket.Services
{
    public interface IBasketService
    {
        Task<Response<BasketDto>> GetBasket(string userId);

        Task<Response<bool>> SaveOrUpdate(BasketDto basketDto);

        Task<Response<bool>> DeleteBasket(string userId);
    }
}
