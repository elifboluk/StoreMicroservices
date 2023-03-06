using FreeProduct.Services.Catalog.DTOs;
using FreeProduct.Services.Catalog.Models;
using FreeProduct.Shared.DTOs;

namespace FreeProduct.Services.Catalog.Services
{
    public interface ICategoryService
    {
        Task<Response<List<CategoryDto>>> GetAllAsync();
        Task<Response<CategoryDto>> CreateAsync(CategoryDto category);
        Task<Response<CategoryDto>> GetByIdAsync(string id);
    }
}
