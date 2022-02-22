using System;
using System.Collections.Generic;
using System.Text;

namespace Product.Application.Interfaces
{
    public interface IProductRepository : IRepository<Product.Domain.AggregateModels.ProductModels.Product>
    {
    }
}
