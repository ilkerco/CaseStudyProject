using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Application.Features.Queries.Models;
using Product.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.Features.Queries.GetProductByProductCode
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductResponseModel>
    {
        IProductRepository _productRepository;

        public GetProductQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductResponseModel> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _productRepository.GetAll().Where(x => x.ProductCode == request.ProductCode).FirstOrDefaultAsync();
                if (product == null)
                    return new ProductResponseModel { IsSuccess = false, ErrMsg = "There is no product with this productCode" };
                return new ProductResponseModel { IsSuccess = true, Price = product.SalePrice, ProductCode = product.ProductCode, Stock = product.Stock };

            }
            catch (Exception ex)
            {
                return new ProductResponseModel { IsSuccess = false, ErrMsg = ex.Message };
            }
        }
    }
}
