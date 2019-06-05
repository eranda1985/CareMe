using Analytics.Model.Dto;
using Analytics.Model.Services;
using CareMe.IntegrationService;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace Analytics.Api.IntegrationEventHandlers
{
	public class NewVehicleAddedEventHandler : IIntegrationEventHandler
	{
		private readonly IService<VehiclesDetailsDto> _service;
		private readonly ILogger<NewVehicleAddedEventHandler> _logger;

		public NewVehicleAddedEventHandler(
			IService<VehiclesDetailsDto> service,
			ILogger<NewVehicleAddedEventHandler> logger
			)
		{
			_service = service;
			_logger = logger;
		}

		public async Task HandleEvent(IntegrationEvent @event)
		{
			_logger.LogDebug("Entering into the handler for New Vehicled Added event in Analytics API");

			if (@event is NewVehicleAddedEvent e)
			{
				_ = await ((VehicleDataService)_service).AddEntry(new VehiclesDetailsDto
				{
					VehicleId = e.VehicleId,
					Rego = e.Rego,
					LastODOMeter = e.LastODOMeter,
					LastUpdated = DateTime.Parse(e.LastUpdated, CultureInfo.GetCultureInfo("en-AU"))
				});
			}
		}
	}
}
