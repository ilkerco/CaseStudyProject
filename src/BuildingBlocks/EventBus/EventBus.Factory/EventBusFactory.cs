using EventBus.Base;
using EventBus.Base.Abstraction;
using EventBust.RabbitMQ;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.Factory
{
    public static class EventBusFactory
    {

        public static IEventBus Create(EventBusConfig config, IServiceProvider serviceProvider)
        {
            return new EventBusRabbitMQ(serviceProvider, config);
        }
    }
}
