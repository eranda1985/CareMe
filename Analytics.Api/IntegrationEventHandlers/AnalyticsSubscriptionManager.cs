using CareMe.IntegrationService;

namespace Analytics.Api.IntegrationEventHandlers
{
	public class AnalyticsSubscriptionManager : ISubscriptionManager
	{
		private readonly IdentityUserAddedEventHandler _identityUserAddedEventHandler;
		private readonly NewVehicleAddedEventHandler _vehicleAddedEventHandler;
		private readonly VehicleDeletedEventHandler _vehicleDeletedEventHandler;

		public AnalyticsSubscriptionManager(
			IdentityUserAddedEventHandler userAddedEventHandler,
			NewVehicleAddedEventHandler vehicleAddedEventHandler,
			VehicleDeletedEventHandler vehicleDeletedEventHandler)
		{
			_identityUserAddedEventHandler = userAddedEventHandler;
			_vehicleAddedEventHandler = vehicleAddedEventHandler;
			_vehicleDeletedEventHandler = vehicleDeletedEventHandler;
		}

		public IIntegrationEventHandler GetEventHandler<T>() where T : IIntegrationEventHandler
		{
			if (typeof(T).Equals(_identityUserAddedEventHandler.GetType()))
			{
				return _identityUserAddedEventHandler;
			}

			if (typeof(T).Equals(_vehicleAddedEventHandler.GetType()))
			{
				return _vehicleAddedEventHandler;
			}

			if (typeof(T).Equals(_vehicleDeletedEventHandler.GetType()))
			{
				return _vehicleDeletedEventHandler;
			}

			return null;
		}
	}
}
