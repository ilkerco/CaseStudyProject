using System;
using System.Collections.Generic;
using System.Text;

namespace Campaign.Application.Models
{
    public class GetProductResponseModel
    {
        public string ProductCode { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string ErrMsg { get; set; }
        public bool IsSuccess { get; set; }
    }
}
