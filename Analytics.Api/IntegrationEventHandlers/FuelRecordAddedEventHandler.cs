using Analytics.Model.Dto;
using Analytics.Model.Services;
using CareMe.IntegrationService;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Analytics.Api.IntegrationEventHandlers
{
	public class FuelRecordAddedEventHandler : IIntegrationEventHandler
	{
		private readonly IService<FuelDetailsDto> _service;
		private readonly ILogger<FuelRecordAddedEventHandler> _logger;

		public FuelRecordAddedEventHandler(
			ILogger<FuelRecordAddedEventHandler> logger,
			IService<FuelDetailsDto> service)
		{
			_service = service;
			_logger = logger;
		}

		public async Task HandleEvent(IntegrationEvent @event)
		{
			_logger.LogDebug("Entering into the handler forFuel Record Added event in Analytics API");

			if (@event is FuelRecordAddedEvent e)
			{
				_ = await ((FuelDataService)_service).AddEntry(new FuelDetailsDto
				{
					Date = DateTime.Parse(e.Date),
					Litres = e.Litres,
					Mileage = e.Mileage,
					Price = e.Price,
					VehicleId = e.VehicleId
				});
			}
		}
	}
}
