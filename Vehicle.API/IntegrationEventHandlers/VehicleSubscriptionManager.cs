using CareMe.IntegrationService;

namespace Vehicle.Api.IntegrationEventHandlers
{
    public class VehicleSubscriptionManager : ISubscriptionManager
	{
		private IdentityUserAddEventHandler _identityUserAddEventHandler;
		private FuelRecordAddedEventHandler _fuelRecordAddedEventHandler;


		public VehicleSubscriptionManager(
			IdentityUserAddEventHandler identityUserAddEventHandler,
			FuelRecordAddedEventHandler fuelRecordAddedEventHandler
			)
		{
			_identityUserAddEventHandler = identityUserAddEventHandler;
			_fuelRecordAddedEventHandler = fuelRecordAddedEventHandler;
		}

		public IIntegrationEventHandler GetEventHandler<T>() where T : IIntegrationEventHandler
		{
			if (typeof(T).Equals(_identityUserAddEventHandler.GetType()))
			{
				return _identityUserAddEventHandler;
			}

			if (typeof(T).Equals(_fuelRecordAddedEventHandler.GetType()))
			{
				return _fuelRecordAddedEventHandler;
			}

			return null;
		}
	}
}
