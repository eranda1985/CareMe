using CareMe.IntegrationService;
using System.Threading.Tasks;
using Vehicle.Model.Dto;
using Vehicle.Model.Services;

namespace Vehicle.Api.IntegrationEventHandlers
{
	public class FuelRecordAddedEventHandler : IIntegrationEventHandler
	{
		private readonly IService<VehicleDataDto> _service;

		public FuelRecordAddedEventHandler(IService<VehicleDataDto> service)
		{
			_service = service;
		}

		public async Task HandleEvent(IntegrationEvent @event)
		{
			if (@event is FuelRecordAddedEvent e)
			{
				var existing = await ((VehicleDataService)_service).GetVehicleById(e.VehicleId);
				existing.ODOMeter = e.Mileage;
				_ = await ((VehicleDataService)_service).Update(existing);
			}
		}
	}
}
