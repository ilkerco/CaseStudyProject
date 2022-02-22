using EventBus.Base.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Campaign.Application.Models
{
    public class OrderCreatedIntegrationEvent : IntegrationEvent
    {
        public string ProductCode { get; set; }
        public int Quantity { get; set; }

        public OrderCreatedIntegrationEvent(string productCode, int quantity)
        {
            ProductCode = productCode;
            Quantity = quantity;
        }
    }
}
