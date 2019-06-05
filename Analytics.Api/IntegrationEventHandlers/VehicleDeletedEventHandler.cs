using Analytics.Model.Dto;
using Analytics.Model.Services;
using CareMe.IntegrationService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Analytics.Api.IntegrationEventHandlers
{
	public class VehicleDeletedEventHandler : IIntegrationEventHandler
	{
		private readonly IService<VehiclesDetailsDto> _service;
		private readonly ILogger<VehicleDeletedEventHandler> _logger;

		public VehicleDeletedEventHandler(
			IService<VehiclesDetailsDto> service,
			ILogger<VehicleDeletedEventHandler> logger
			)
		{
			_service = service;
			_logger = logger;
		}


		public async Task HandleEvent(IntegrationEvent @event)
		{
			_logger.LogDebug("Event handler triggered for vehicle deleted event in Analytics API");

			if (@event is VehicleDeletedEvent e)
			{
				_ = await ((VehicleDataService)_service).DeleteEntry(e.VehicleId);
			}
		}
	}
}
