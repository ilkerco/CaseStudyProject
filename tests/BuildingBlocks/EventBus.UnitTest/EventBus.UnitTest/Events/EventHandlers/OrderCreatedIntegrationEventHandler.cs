using EventBus.Base.Abstraction;
using EventBus.UnitTest.Events.Event;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.UnitTest.Events.EventHandlers
{
    public class OrderCreatedIntegrationEventHandler : IIntegrationEventHandler<OrderCreatedIntegrationEvent>
    {
       
        public Task Handle(OrderCreatedIntegrationEvent @event)
        {
            return Task.CompletedTask;
        }
    }
}
