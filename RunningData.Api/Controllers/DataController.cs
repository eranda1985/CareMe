using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RunningData.Api.ActionFilters;
using RunningData.Api.Parameters;
using RunningData.Model.Dto;
using RunningData.Model.Services;
using System;
using System.Globalization;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RunningData.Api.Controllers
{
	[Route("fueldata")]
	[Produces("application/json")]
	[ApiVersion("1.0")]
	public class DataController : Controller
	{
		private readonly IService<FuelDataDto> _fuelService;
		private readonly IConfiguration _configuration;
		private readonly ILogger<DataController> _logger;

		public DataController(ILogger<DataController> logger, IService<FuelDataDto> fuelService, IConfiguration configuration)
		{
			_logger = logger;
			_fuelService = fuelService;
			_configuration = configuration;
		}

		// POST: fueldata/add
		[HttpPost]
		[Route("add")]
		[ProducesResponseType(200, Type = typeof(bool))]
		[MapToApiVersion("1.0")]
		[ServiceFilter(typeof(AuthorizeUserTokenAttribute))]
		public async Task<bool> InsertFuelData([FromBody]FuelDataRequest request)
		{
			_logger.LogDebug("Entering InsertFuelData method.");
			var res = await ((FuelDataService)_fuelService).InsertAsync(DateTime.Parse(request.Date, CultureInfo.GetCultureInfo("en-AU"))
					, request.Litres,
					request.Price,
					request.Mileage,
					request.VehicleId);
			return res;
		}
	}
}
