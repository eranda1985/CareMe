using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareMe.IntegrationService
{
	public interface ISubscriptionManager
	{
		IIntegrationEventHandler GetEventHandler<T>() where T : IIntegrationEventHandler;
	}
}
