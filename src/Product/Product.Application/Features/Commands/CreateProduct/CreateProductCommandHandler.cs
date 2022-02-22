using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Application.Features.Commands.Models;
using Product.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Features.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductResponse>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<CreateProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var isProductExis = await _productRepository.GetAll().Where(x=>x.ProductCode == request.ProductCode).FirstOrDefaultAsync();
                if (isProductExis!=null)
                    return new CreateProductResponse { ErrMsg = "This productCode is already in use", IsSuccess = false };
                var product = new Product.Domain.AggregateModels.ProductModels.Product(
                    request.ProductCode,
                    request.Price,
                    request.Stock);
                await _productRepository.AddAsync(product);
                var isSuccess = await _productRepository.SaveChanges() > 0;
                if (isSuccess)
                    return new CreateProductResponse { data = new CreateProductRequest {Price = product.SalePrice,ProductCode = request.ProductCode,Stock=request.Stock }, IsSuccess = true };
                return new CreateProductResponse { ErrMsg = "Error while creating product", IsSuccess = false };

            }
            catch (Exception ex)
            {
                return new CreateProductResponse { ErrMsg = ex.Message, IsSuccess = false };
            }
        }
    }
}
