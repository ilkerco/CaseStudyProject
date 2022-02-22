using System;
using System.Collections.Generic;
using System.Text;

namespace Product.Application.Features.Commands.Models
{
    public class CreateProductRequest
    {
        public string ProductCode { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
