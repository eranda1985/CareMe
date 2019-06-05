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
		NewVehicleAddedEventHandler _vehicleAddedEventHandler;

		public AnalyticsSubscriptionManager(
			IdentityUserAddedEventHandler userAddedEventHandler,
			NewVehicleAddedEventHandler vehicleAddedEventHandler)
		{
			_identityUserAddedEventHandler = userAddedEventHandler;
			_vehicleAddedEventHandler = vehicleAddedEventHandler;
		}

		public IIntegrationEventHandler GetEventHandler<T>() where T : IIntegrationEventHandler
		{
			if(typeof(T).Equals(_identityUserAddedEventHandler.GetType()))
			{
				return _identityUserAddedEventHandler;
			}

			if (typeof(T).Equals(_vehicleAddedEventHandler.GetType()))
			{
				return _identityUserAddedEventHandler;
			}


			return null;
		}
	}
}
