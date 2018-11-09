using CareMe.IntegrationService;
using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareMe.RabbitMQIntegrationService
{
	public class RabbitMQSubscriptionManager : ISubscriptionManager
	{
		private IdentityUserAddEventHandler _identityUserAddEventHandler;
		private IBus _bus;

		IBus ISubscriptionManager.Bus { get => _bus; set => _bus = value; }

		public IIntegrationEventHandler GetEventHandler<T>() where T : IIntegrationEventHandler
		{
			if (typeof(T).Equals(_identityUserAddEventHandler.GetType()))
			{
				return _identityUserAddEventHandler;
			}

			return null;
		}
	}
}
