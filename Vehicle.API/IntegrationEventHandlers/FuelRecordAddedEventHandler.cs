using CareMe.IntegrationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vehicle.API.IntegrationEventHandlers
{
	public class FuelRecordAddedEventHandler : IIntegrationEventHandler
	{
		public Task HandleEvent(IntegrationEvent @event)
		{
			throw new NotImplementedException();
		}
	}
}
