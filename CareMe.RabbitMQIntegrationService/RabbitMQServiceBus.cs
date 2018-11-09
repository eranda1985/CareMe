using CareMe.IntegrationService;
using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CareMe.RabbitMQIntegrationService
{
    public class RabbitMQServiceBus : IServiceBus
    {
        public void Publish<T>(T @event) where T: IntegrationEvent
        {
            using (var bus = RabbitHutch.CreateBus("host=localhost"))
            {
                bus.Publish<T>(@event);
            }
        }

        public void Subscribe<T, H>(string subscriptionId)
            where T : IntegrationEvent
            where H : IIntegrationEventHandler
        {
            var handler = (H)Assembly.GetCallingAssembly().CreateInstance(typeof(H).FullName);

            using (var bus = RabbitHutch.CreateBus("host=localhost"))
            {
                bus.Subscribe<T>(subscriptionId, handler.HandleEvent);
            }
        }
    }
}
