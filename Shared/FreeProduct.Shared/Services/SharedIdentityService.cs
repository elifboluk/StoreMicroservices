using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace FreeProduct.Shared.Services
{
    public class SharedIdentityService : ISharedIdentityService
    {
        private IHttpContextAccessor _httpContextAccessor; // IHttpContextAccessor ile hem request'e hem de response'a erişmek mümkün.

        public SharedIdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        // 1.adım
        // public string GetUserId => _httpContextAccessor.HttpContext.User.Claims.Where(x=>x.Type == "sub").FirstOrDefault().Value;

        // best practices
        public string GetUserId => _httpContextAccessor.HttpContext.User.FindFirst("sub").Value; // type-value --> claim
    }
}
