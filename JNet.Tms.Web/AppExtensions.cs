using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;

namespace JNet.Tms
{
    internal static class AppExtensions
    {
        public static IMvcBuilder ProvideDbContextEntities(this IMvcBuilder builder)
        {
            var types = builder.PartManager.ApplicationParts
                .OfType<IApplicationPartTypeProvider>()
                .SelectMany(p => p.Types.Where(p => AppModule.IsEntity(p)));

            var groupd = types.GroupBy(p => p.Assembly).ToList();

            IEnumerable<EntityTypeBuilder> ProvideEntities(ModelBuilder modelBuilder)
            {
                foreach (var type in groupd.First(g => g.Key == typeof(Users.User).Assembly))
                {
                    yield return modelBuilder.Entity(type).ToTable("C_" + type.Name);
                }

                foreach (var type in groupd.First(g => g.Key == typeof(Vms.Vehicle).Assembly)
                                           .Where(t => t.BaseType != typeof(Vms.VehicleDict)))
                {
                    yield return modelBuilder.Entity(type).ToTable("Vms_" + type.Name);
                }

                foreach (var type in groupd.First(g => g.Key == typeof(Wbms.Waybill).Assembly))
                {
                    yield return modelBuilder.Entity(type).ToTable("Wb_" + type.Name);
                }
            }

            AppDbContext.GetEntityTypes = ProvideEntities;

            return builder;
        }
    }
}
