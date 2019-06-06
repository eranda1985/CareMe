using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Analytics.Api.ActionFilters;
using Analytics.Model.Dto;
using Analytics.Model.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Analytics.Api.Controllers
{
	[Route("analytics")]
	[ApiController]
	[ApiVersion("1.0")]
	public class AnalyticsController : ControllerBase
	{

		private IService<FuelDetailsDto> _fuelService;
		private IService<VehiclesDetailsDto> _vehicleService;
		private IConfiguration _configuration;
		private ILogger<AnalyticsController> _logger;

		public AnalyticsController(
			IService<FuelDetailsDto> fuelService,
			IService<VehiclesDetailsDto> vehicleService,
			IConfiguration configuration,
			ILogger<AnalyticsController> logger)
		{
			_fuelService = fuelService;
			_configuration = configuration;
			_logger = logger;
			_vehicleService = vehicleService;
		}

		//	GET: analytics/fuel/recentdata/{vehicleId}
		[HttpGet]
		[Route("fuel/recentdata/{vehicleId}")]
		[ProducesResponseType(200, Type = typeof(List<FuelDetailsDto>))]
		[MapToApiVersion("1.0")]
		[ServiceFilter(typeof(AuthorizeUserTokenAttribute))]

		public async Task<IActionResult> GetFuelConsumptionRecent(long vehicleId)
		{
			var res = await ((FuelDataService)_fuelService).GetRecentEntries(vehicleId);
			return Ok(res);
		}

		//	GET: analytics/vehicle/{vehicleId}/lastmile
		[HttpGet]
		[Route("vehicle/{vehicleId}/lastmile")]
		[ProducesResponseType(200, Type = typeof(VehiclesDetailsDto))]
		[MapToApiVersion("1.0")]
		[ServiceFilter(typeof(AuthorizeUserTokenAttribute))]

		public async Task<IActionResult> GetLastMileInfo(long vehicleId)
		{
			var res = await ((VehicleDataService)_vehicleService).GetVehicleById(vehicleId);
			return Ok(res);
		}

		//	GET: analytics/fuel/backward/{vehicleId}?seeddate=
		[HttpGet]
		[Route("fuel/backward/{vehicleId}")]
		[ProducesResponseType(200, Type = typeof(List<FuelDetailsDto>))]
		[MapToApiVersion("1.0")]
		[ServiceFilter(typeof(AuthorizeUserTokenAttribute))]

		public async Task<IActionResult> GetFuelConsumptionBackward(string seeddate, long vehicleId)
		{
			var seedDate = DateTime.Parse(seeddate, CultureInfo.GetCultureInfo("en-AU"));
			var res = await ((FuelDataService)_fuelService).GetBackwardEntries(seedDate, vehicleId);
			return Ok(res);
		}

		//	GET: analytics/fuel/forward/{vehicleId}?seeddate=
		[HttpGet]
		[Route("fuel/forward/{vehicleId}")]
		[ProducesResponseType(200, Type = typeof(List<FuelDetailsDto>))]
		[MapToApiVersion("1.0")]
		[ServiceFilter(typeof(AuthorizeUserTokenAttribute))]

		public async Task<IActionResult> GetFuelConsumptionForward(string seeddate, long vehicleId)
		{
			var seedDate = DateTime.Parse(seeddate, CultureInfo.GetCultureInfo("en-AU"));
			var res = await ((FuelDataService)_fuelService).GetForwardEntries(seedDate, vehicleId);
			return Ok(res);
		}
	}
}