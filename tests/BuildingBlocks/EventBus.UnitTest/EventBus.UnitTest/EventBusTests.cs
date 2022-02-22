using EventBus.Base;
using EventBus.Base.Abstraction;
using EventBus.Factory;
using EventBus.UnitTest.Events.Event;
using EventBus.UnitTest.Events.EventHandlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using RabbitMQ.Client;

namespace EventBus.UnitTest
{
    public class EventBusTests
    {
        private ServiceCollection services;

        [SetUp]
        public void Setup()
        {
            services = new ServiceCollection();
            services.AddLogging(configure => configure.AddConsole());

            
        }

        [Test]
        public void subscribe_event_on_rabbitmq_test()
        {
            services.AddSingleton<IEventBus>(sp =>
            {
                EventBusConfig config = new EventBusConfig
                {
                    ConnectionRetryCount = 5,
                    SubscriberClientAppName = "EventBus.UnitTest",
                    DefaultTopicName = "CaseStudyTopicName",
                    EventBusType = EventBusType.RabbitMQ,
                    EventNameSuffix = "IntegrationEvent",
                    /*Connection = new ConnectionFactory()
                    {
                        HostName = "localhost",
                        Port = 5672,
                        UserName = "guest",
                        Password ="guest"
                    }*/
                };


                return EventBusFactory.Create(config, sp);
            });


            var sp = services.BuildServiceProvider();

            var eventBus = sp.GetRequiredService<IEventBus>();

            eventBus.Subscribe<OrderCreatedIntegrationEvent, OrderCreatedIntegrationEventHandler>();
            //eventBus.UnSubscribe<OrderCreatedIntegrationEvent, OrderCreatedIntegrationEventHandler>();
            eventBus.Publish(new OrderCreatedIntegrationEvent(1));
        }
    }
}