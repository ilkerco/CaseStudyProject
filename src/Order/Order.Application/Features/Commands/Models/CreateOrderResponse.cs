using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Application.Features.Commands.Models
{
    public class CreateOrderResponse
    {
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrMsg { get; set; }
    }
}
