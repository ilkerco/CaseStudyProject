using EventBus.Base.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Application.Events
{
    public class OrderCreatedIntegrationEvent : IntegrationEvent
    {
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public OrderCreatedIntegrationEvent(string productCode, int quantity,decimal price)
        {
            ProductCode = productCode;
            Quantity = quantity;
            Price = price;
        }
    }
}
