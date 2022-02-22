using EventBus.Base.Abstraction;
using EventBus.Base.SubManagers;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Base.Events
{
    public abstract class BaseEventBus : IEventBus
    {
        public readonly IServiceProvider ServiceProvider;
        public readonly IEventBusSubscriptionManager SubsManager;

        public EventBusConfig eventBusConfig;

        protected BaseEventBus(IServiceProvider serviceProvider, EventBusConfig config)
        {
            ServiceProvider = serviceProvider;
            eventBusConfig = config;
            SubsManager = new InMemoryEventBusSubscriptionManager(ProcessEventName);
        }


        public virtual string ProcessEventName(string eventName)
        {
            if(eventName == "UpdateProductIntegrationEvent" || eventName == "UpdateProduct")
            {
                //eventName = eventName.TrimEnd("IntegrationEvent".ToArray()); this trim method is nnot working for this string :) idk why
                eventName = "UpdateProduct";
                return eventName;
            }
            if (eventName == "CampaignOverIntegrationEvent" || eventName == "CampaignOver")
            {
                //eventName = eventName.TrimEnd("IntegrationEvent".ToArray()); this trim method is nnot working for this string :) idk why
                eventName = "CampaignOver";
                return eventName;
            }
            if (eventBusConfig.DeleteEventPrefix)
            {
                eventName = eventName.TrimStart(eventBusConfig.EventNamePrefix.ToArray());
            }
            if (eventBusConfig.DeleteEventNameSuffix)
            {
                eventName = eventName.TrimEnd(eventBusConfig.EventNameSuffix.ToArray());
            }
            return eventName;
        }

        public virtual string GetSubName(string eventName)
        {
            var gg = eventBusConfig.SubscriberClientAppName + ProcessEventName(eventName);

            return $"{eventBusConfig.SubscriberClientAppName}.{ProcessEventName(eventName)}";
        }
        

        public virtual void Dispose()
        {
            eventBusConfig = null;
        }
        public async Task<bool> ProcessEvent(string eventName,string message)
        {
            eventName = ProcessEventName(eventName);
            var processed = false;
            if (SubsManager.HasSubscriptionsForEvent(eventName))
            {
                var subscriptions = SubsManager.GetHandlersForEvent(eventName);
                using(var scope = ServiceProvider.CreateScope())
                {
                    foreach (var subscription in subscriptions)
                    {
                        try
                        {
                            var handler = ServiceProvider.GetService(subscription.HandlerType);
                            if (handler == null) continue;

                            var eventType = SubsManager.GetEventTypeByName($"{eventBusConfig.EventNamePrefix}{eventName}{eventBusConfig.EventNameSuffix}");
                            var integrationEvent = JsonConvert.DeserializeObject(message, eventType);

                            var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);
                            await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { integrationEvent });
                        }
                        catch(Exception ex)
                        {

                        }
                        /*var handler = ServiceProvider.GetService(subscription.HandlerType);
                        if (handler == null) continue;

                        var eventType = SubsManager.GetEventTypeByName($"{eventBusConfig.EventNamePrefix}{eventName}{eventBusConfig.EventNameSuffix}");
                        var integrationEvent = JsonConvert.DeserializeObject(message, eventType);

                        var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);
                        await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { integrationEvent });*/
                    }
                }
                processed = true;
            }
            return processed;
        }

        public abstract void Publish(IntegrationEvent @event);
        public abstract void Subscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>;
        public abstract void UnSubscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>;
    }
}
