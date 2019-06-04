using CareMe.IntegrationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analytics.Api.IntegrationEventHandlers
{
	public class AnalyticsSubscriptionManager : ISubscriptionManager
	{
		private IdentityUserAddedEventHandler _identityUserAddedEventHandler;

		public AnalyticsSubscriptionManager(IdentityUserAddedEventHandler handler )
		{
			_identityUserAddedEventHandler = handler;
		}

		public IIntegrationEventHandler GetEventHandler<T>() where T : IIntegrationEventHandler
		{
			if(typeof(T).Equals(_identityUserAddedEventHandler.GetType()))
			{
				return _identityUserAddedEventHandler;
			}

			return null;
		}
	}
}
