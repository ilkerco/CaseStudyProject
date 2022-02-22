using Product.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.API.Services.Interfaces
{
    public interface IProductService
    {
        Task<CreateProductResponse> CreateProduct(CreateProductRequest addProductRequest);
        Task<GetProductResponse> GetProductInfo(string productCode);
        Task<bool> UpdateProductStock(string productCode, int quantity);
    }
}
