using MediatR;
using Product.Application.Features.Queries.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Product.Application.Features.Queries.GetProductByProductCode
{
    public class GetProductQuery : IRequest<ProductResponseModel>
    {
        public string ProductCode { get; set; }

        public GetProductQuery(string productCode)
        {
            ProductCode = productCode;
        }
    }
}
