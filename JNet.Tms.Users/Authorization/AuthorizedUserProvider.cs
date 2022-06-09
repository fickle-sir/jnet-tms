using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;

namespace JNet.Tms
{
    public class AuthorizedUserProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private AuthorizedUser _user;

        public AuthorizedUserProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public AuthorizedUser GetUser()
        {
            return _user ?? (_user = DeserializeClaims<AuthorizedUser>(_httpContextAccessor.HttpContext.User.Claims));
        }

        private static T DeserializeClaims<T>(IEnumerable<Claim> claims) where T : class
        {
            var targetType = typeof(T);
            var properties = typeof(AuthorizedUser).GetProperties();
            var instance = Activator.CreateInstance<T>();

            foreach (var claim in claims)
            {
                var property = properties.FirstOrDefault(p => p.Name == claim.Type);
                if (property != null)
                {
                    if (property.PropertyType == typeof(string))
                    {
                        property.SetValue(instance, claim.Value);
                    }
                    else
                    {
                        var converter = TypeDescriptor.GetConverter(property.PropertyType);
                        if (converter.CanConvertFrom(typeof(string)))
                        {
                            var value = converter.ConvertTo(claim.Value, property.PropertyType);
                            property.SetValue(instance, value);
                        }
                        else
                        {
                            throw new InvalidCastException($"can not convert the value from {typeof(string)} to {property.PropertyType.Name} of {targetType.FullName}.{property.Name}");
                        }
                    }
                }
            }

            return instance;
        }
    }
}
