using EventBus.Base.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.UnitTest.Events.Event
{
    public class OrderCreatedIntegrationEvent:IntegrationEvent
    {
        public int OrderId { get; set; }

        public OrderCreatedIntegrationEvent(int id)
        {
            OrderId = id;
        }
    }
}
