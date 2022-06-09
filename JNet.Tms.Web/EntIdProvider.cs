using System;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace JNet.Tms
{
    internal class EntIdProvider : IEntityPropertyProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EntIdProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool GetProperty(Type type, out PropertyInfo property, out object value)
        {
            if (typeof(IEntId).IsAssignableFrom(type))
            {
                var provider = _httpContextAccessor.HttpContext.RequestServices.GetRequiredService<AuthorizedUserProvider>();
                property = type.GetProperty(nameof(IEntId.EntId));
                value = provider.GetUser().EntId;
                return true;
            }

            property = null;
            value = null;
            return false;
        }
    }
}
