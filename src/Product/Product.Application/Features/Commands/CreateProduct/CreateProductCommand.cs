using MediatR;
using Product.Application.Features.Commands.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Product.Application.Features.Commands.CreateProduct
{
    public class CreateProductCommand :IRequest<CreateProductResponse>
    {
        public string ProductCode { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public CreateProductCommand(string productCode, decimal price, int stock)
        {
            ProductCode = productCode;
            Price = price;
            Stock = stock;
        }
    }
}
