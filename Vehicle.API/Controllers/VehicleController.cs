using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vehicle.Api.ActionFilters;
using Vehicle.API.Parameters;
using Vehicle.Model.Dto;
using Vehicle.Model.Services;

namespace Vehicle.API.Controllers
{
    [Route("vehicle")]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    public class VehicleController : Controller
    {
        IService<VehicleDataDto> _service;

        public VehicleController(IService<VehicleDataDto> service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("add")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [MapToApiVersion("1.0")]
        [ServiceFilter(typeof(AuthorizeUserTokenAttribute))]
        public async Task<IActionResult> AddVehicle([FromBody]VehicleDataAddRequest request)
        {
            var res = await ((VehicleDataService)_service).
                AddVehicle(request.VehicleType,
                request.Brand,
                request.Model,
                request.FuelType,
                request.RegoPlate,
                DateTime.Parse(request.Date, CultureInfo.GetCultureInfo("en-AU")),
                request.ODOMeter);

            return Ok(res);
        }
    }
}