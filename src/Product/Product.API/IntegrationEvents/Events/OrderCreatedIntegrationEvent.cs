using EventBus.Base.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.API.IntegrationEvents.Events
{
    public class OrderCreatedIntegrationEvent:IntegrationEvent
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
