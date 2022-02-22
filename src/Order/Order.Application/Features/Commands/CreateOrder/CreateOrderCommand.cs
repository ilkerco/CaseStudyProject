using MediatR;
using Order.Application.Features.Commands.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Application.Features.Commands.CreateOrder
{
    public class CreateOrderCommand:IRequest<CreateOrderResponse>
    {
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
        public decimal ProductPrice { get; set; }

        public CreateOrderCommand(string productCode, int quantity,decimal productPrice)
        {
            ProductCode = productCode;
            Quantity = quantity;
            ProductPrice = productPrice;
        }
    }
}
