using CareMe.IntegrationService;
using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CareMe.RabbitMQIntegrationService
{
	public class RabbitMQServiceBus : IServiceBus, IDisposable
	{
		private ISubscriptionManager _subscriptionManager;
		private IBus _bus;

		public RabbitMQServiceBus()
		{
			if (_bus == null)
			{
				_bus = RabbitHutch.CreateBus("host=localhost");
			}
		}

		public RabbitMQServiceBus(ISubscriptionManager subscriptionManager)
		{
			if (_bus == null)
			{
				_bus = RabbitHutch.CreateBus("host=localhost");
			}
			_subscriptionManager = subscriptionManager;
		}
		public void Publish<T>(T @event) where T : IntegrationEvent
		{
			_bus.Publish<T>(@event);
		}

		public void Subscribe<T, H>(string subscriptionId)
				where T : IntegrationEvent
				where H : IIntegrationEventHandler
		{
			var handler = _subscriptionManager.GetEventHandler<H>();
			
			_bus.SubscribeAsync<T>(subscriptionId, handler.HandleEvent);
		}

		public IBus GetBus()
		{
			if (_bus == null)
			{
				_bus = RabbitHutch.CreateBus("host=localhost");
			}
			return _bus;
		}

		public void Dispose()
		{
			_bus.Dispose();
		}
	}
}
