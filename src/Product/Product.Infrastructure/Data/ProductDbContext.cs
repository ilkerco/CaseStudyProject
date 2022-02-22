using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Product.Infrastructure.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Product.Domain.AggregateModels.ProductModels.Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Product.Domain.AggregateModels.ProductModels.Product>()
            .Property(p => p.Price)
            .HasColumnType("decimal(18,2)");
        }
    }
}
