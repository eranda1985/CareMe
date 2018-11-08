using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RunningData.Api.Parameters;
using RunningData.Model.Dto;
using RunningData.Model.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RunningData.Api.Controllers
{
    [Route("postdata")]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    public class DataController : Controller
    {
        private IService<FuelDataDto> _fuelService;
        private IConfiguration _configuration;
        private ILogger<DataController> _logger;

        public DataController(ILogger<DataController> logger, IService<FuelDataDto> fuelService, IConfiguration configuration)
        {
            _logger = logger;
            _fuelService = fuelService;
            _configuration = configuration;
        }
        
        // POST: data/fuel
        [HttpPost]
        [Route("fuel")]
        [ProducesResponseType(200, Type=typeof(bool))]
        [MapToApiVersion("1.0")]
        public async Task<bool> InsertFuelData([FromBody]FuelDataRequest request)
        {
            _logger.LogDebug("Entering InsertFuelData method.");
            var res = await ((FuelDataService)_fuelService).InsertAsync(request.Date, request.Litres, request.Price, request.Mileage);
            return res;
        }
    }
}
