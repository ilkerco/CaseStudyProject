using Product.Application.Interfaces;
using Product.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Product.Infrastructure.Repository
{
    public class ProductRepository : Repository<Product.Domain.AggregateModels.ProductModels.Product>, IProductRepository
    {
        public ProductRepository(ProductDbContext context) : base(context)
        {

        }
    }
}
