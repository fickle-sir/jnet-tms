using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JNet
{
    public class AppDbContext : DbContext
    {
        public static DbContextOptions<AppDbContext> Options { get; set; }

        public static Func<ModelBuilder, IEnumerable<EntityTypeBuilder>> GetEntityTypes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (GetEntityTypes == null)
                throw new ArgumentException($"{nameof(GetEntityTypes)} is null");
            var entityTypes = typeof(AppDbContext).Assembly.GetTypes().Where(type => type.IsEntity());
            modelBuilder.ApplyConfigurations(GetEntityTypes(modelBuilder));
        }

        public static AppDbContext Create() => new AppDbContext(Options);
    }
}
