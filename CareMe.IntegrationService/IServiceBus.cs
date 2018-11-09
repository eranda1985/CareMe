using System;
using System.Collections.Generic;
using System.Text;

namespace CareMe.IntegrationService
{
    public interface IServiceBus
    {
        void Publish<T>(T @event) where T : IntegrationEvent;
        void Subscribe<T, H>(string id) where T : IntegrationEvent where H : IIntegrationEventHandler;
    }
}
