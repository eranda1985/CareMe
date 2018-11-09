using CareMe.IntegrationService;
using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningData.Api.IntegrationEventHandlers
{
	public class RunningDataSubscriptionManager : ISubscriptionManager
	{
		private IdentityUserAddEventHandler _identityUserAddEventHandler;
		
		public RunningDataSubscriptionManager(IdentityUserAddEventHandler identityUserAddEventHandler)
		{
			_identityUserAddEventHandler = identityUserAddEventHandler;
		}

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
