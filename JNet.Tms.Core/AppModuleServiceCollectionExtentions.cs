using System;
using System.Collections.Generic;
using System.Linq;
using JNet;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AppModuleServiceCollectionExtentions
    {
        public static IMvcBuilder AddAppModule(this IMvcBuilder builder)
        {
            var list = new List<Type>();

            foreach (var part in builder.PartManager.ApplicationParts.OfType<IApplicationPartTypeProvider>())
            {
                foreach (var type in part.Types)
                {
                    if (AppModule.IsEntity(type))
                        list.Add(type);
                }
            }

            builder.Services.TryAddSingleton(new AppModule(list));

            return builder;
        }
    }
}
