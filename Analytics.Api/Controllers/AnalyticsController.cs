using System;
using System.Collections.Generic;
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
		private IConfiguration _configuration;
		private ILogger<AnalyticsController> _logger;

		public AnalyticsController(
			IService<FuelDetailsDto> fuelService,
			IConfiguration configuration,
			ILogger<AnalyticsController> logger)
		{
			_fuelService = fuelService;
			_configuration = configuration;
			_logger = logger;
		}

		//	GET: analytics/fuel/recent
		[HttpGet]
		[Route("recentdata")]
		[ProducesResponseType(200, Type = typeof(List<FuelDetailsDto>))]
		[MapToApiVersion("1.0")]
		[ServiceFilter(typeof(AuthorizeUserTokenAttribute))]

		public async Task<IActionResult> GetFuelConsumptionRecent()
		{
			var res = await ((FuelDataService)_fuelService).GetRecentEntries();
			return Ok(res);
		}
	}
}