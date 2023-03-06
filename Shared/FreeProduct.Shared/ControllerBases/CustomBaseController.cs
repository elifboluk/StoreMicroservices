using FreeProduct.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FreeProduct.Shared.ControllerBases
{
    public class CustomBaseController : ControllerBase
    {
        public IActionResult CreateActionResultInstance<T>(Response<T> response)
        {
            return new ObjectResult(response) // Return value from response
            {
                StatusCode = response.StatusCode
            };
        }
    }
}
