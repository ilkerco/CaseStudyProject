using System;
using System.Collections.Generic;
using System.Text;

namespace Product.Application.Features.Commands.Models
{
    public class CreateProductResponse
    {
        public bool IsSuccess { get; set; }
        public CreateProductRequest data { get; set; }
        public string ErrMsg { get; set; }
    }
}
