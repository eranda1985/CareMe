using CareMe.IntegrationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analytics.Api.IntegrationEventHandlers
{
	public class AnalyticsSubscriptionManager : ISubscriptionManager
	{

		public IIntegrationEventHandler GetEventHandler<T>() where T : IIntegrationEventHandler
		{
			throw new NotImplementedException();
		}
	}
}
