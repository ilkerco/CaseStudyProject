using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.Application.Models
{
    public class CreateOrderRequest
    {
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
        
    }
}
