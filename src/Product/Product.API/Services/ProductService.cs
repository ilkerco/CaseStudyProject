using Microsoft.EntityFrameworkCore;
using Product.API.Models;
using Product.API.Services.Interfaces;
using Product.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<CreateProductResponse> CreateProduct(CreateProductRequest addProductRequest)
        {
            try
            {
                var isProductExis = await GetProductInfo(addProductRequest.ProductCode);
                if (isProductExis.IsSuccess)
                    return new CreateProductResponse { ErrMsg = "This productCode is already in use", IsSuccess = false };
                var product = new Product.Domain.AggregateModels.ProductModels.Product(
                    addProductRequest.ProductCode,
                    addProductRequest.Price,
                    addProductRequest.Stock);
                await _productRepository.AddAsync(product);
                var isSuccess = await _productRepository.SaveChanges() > 0;
                if (isSuccess)
                    return new CreateProductResponse { data = addProductRequest, IsSuccess = true };
                return new CreateProductResponse { ErrMsg = "Error while creating product", IsSuccess = false };

            }
            catch (Exception ex)
            {
                return new CreateProductResponse { ErrMsg = ex.Message, IsSuccess = false };
            }
        }

        public async Task<GetProductResponse> GetProductInfo(string productCode)
        {
            try
            {
                var product = await _productRepository.GetAll().Where(x => x.ProductCode == productCode).FirstOrDefaultAsync();
                if (product == null)
                    return new GetProductResponse { IsSuccess = false, ErrMsg = "There is no product with this productCode" };
                return new GetProductResponse { IsSuccess = true, Price = product.Price, ProductCode = product.ProductCode, Stock = product.Stock };

            }
            catch (Exception ex)
            {
                return new GetProductResponse { IsSuccess = false, ErrMsg = ex.Message };
            }
        }

        public async Task<bool> UpdateProductStock(string productCode, int quantity)
        {
            try
            {
                var product = await _productRepository.GetAll().Where(x => x.ProductCode == productCode).FirstOrDefaultAsync();
                product.UpdateStock(quantity);
                await _productRepository.UpdateAsync(product, product.Id);
                await _productRepository.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
