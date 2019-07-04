using CareMe.IntegrationService;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Vehicle.Model.Dto;
using Vehicle.Model.Services;

namespace Vehicle.Api.IntegrationEventHandlers
{
	public class FuelRecordAddedEventHandler : IIntegrationEventHandler
	{
		private readonly VehicleDataSubscriberService _service;

		public FuelRecordAddedEventHandler(VehicleDataSubscriberService service)
		{
			_service = service;
		}

		public async Task HandleEvent(IntegrationEvent @event)
		{
			if (@event is FuelRecordAddedEvent e)
			{
				var existing = await _service.GetVehicleById(e.VehicleId);
				existing.ODOMeter = e.Mileage;
				_ = await _service.Update(existing);
			}
		}
	}
}
