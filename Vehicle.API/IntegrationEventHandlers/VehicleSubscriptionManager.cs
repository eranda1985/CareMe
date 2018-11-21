using CareMe.IntegrationService;

namespace Vehicle.Api.IntegrationEventHandlers
{
    public class VehicleSubscriptionManager : ISubscriptionManager
	{
		private IdentityUserAddEventHandler _identityUserAddEventHandler;
		
		public VehicleSubscriptionManager(IdentityUserAddEventHandler identityUserAddEventHandler)
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
