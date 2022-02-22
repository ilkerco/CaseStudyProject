using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.API.Models
{
    public class CreateProductResponse
    {
        public bool IsSuccess { get; set; }
        public CreateProductRequest data { get; set; }
        public string ErrMsg { get; set; }
    }
}
