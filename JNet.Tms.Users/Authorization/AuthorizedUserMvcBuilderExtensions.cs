using System;
using Microsoft.Extensions.DependencyInjection.Extensions;
using JNet.Tms;
using JNet.Tms.Users;
using JNet.Tms.ModelBinding.Binders;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AuthorizedUserMvcBuilderExtensions
    {
        public static IMvcBuilder AddAuthorizedUser(this IMvcBuilder builder)
        {
            var services = builder.Services;

            // dependent IHttpContextAccessor(Transient Servcie)
            services.TryAddScoped<AuthorizedUserProvider, AuthorizedUserProvider>();

            builder.AddMvcOptions(options =>
            {
                options.ReplaceBodyModelBinder();
                //adds to after the ServicesModelBinderProvider(index:1).
                options.ModelBinderProviders.Insert(2, new FromUIDModelBinderProvider());
                options.ModelBinderProviders.Insert(3, new FromEntIdModelBinderProvider());
            });

            return builder;
        }
    }
}
