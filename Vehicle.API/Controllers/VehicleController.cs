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

		// POST: vehicle/add
		[HttpPost]
		[Route("add")]
		[ProducesResponseType(200, Type = typeof(VehicleDataDto))]
		[MapToApiVersion("1.0")]
		//[ServiceFilter(typeof(AuthorizeUserTokenAttribute))]
		public async Task<IActionResult> AddVehicle([FromBody]VehicleDataAddRequest request)
		{
			var res = await ((VehicleDataService)_service).
					AddVehicle(request.VehicleType,
					request.Brand,
					request.Model,
					request.FuelType,
					request.RegoPlate,
					DateTime.Parse(request.Date, CultureInfo.GetCultureInfo("en-AU")),
					request.ODOMeter,
					request.Username);

			return Ok(res);
		}

		// POST: vehicle/setdefault
		[HttpPost]
		[Route("setdfault/{rego}/{username}")]
		[ProducesResponseType(200, Type = typeof(bool))]
		[MapToApiVersion("1.0")]
		//[ServiceFilter(typeof(AuthorizeUserTokenAttribute))]
		public async Task<IActionResult> SetDefault(string rego, string username)
		{
			var res = await ((VehicleDataService)_service).SetDefaultVehicle(rego, username);

			return Ok(res);
		}

		// PUT: vehicle/update
		[HttpPut]
		[Route("update")]
		[ProducesResponseType(200, Type = typeof(bool))]
		[MapToApiVersion("1.0")]
		//[ServiceFilter(typeof(AuthorizeUserTokenAttribute))]
		public async Task<IActionResult> Update([FromBody]VehicleDataUpdateRequest request)
		{
			var res = await ((VehicleDataService)_service).Update(new VehicleDataDto
			{
				Id = request.Id,
				Brand = request.Brand,
				Date = DateTime.Parse(request.Date, CultureInfo.GetCultureInfo("en-AU")),
				FuelType = request.FuelType,
				Model = request.Model,
				ODOMeter = request.ODOMeter,
				RegoPlate = request.RegoPlate,
				VehicleType = request.VehicleType
			});

			return Ok(res);
		}

		// GEt: vehicle/get/{username}
		[HttpGet]
		[Route("get/{username}")]
		[ProducesResponseType(200, Type = typeof(List<VehicleDataDto>))]
		[MapToApiVersion("1.0")]
		//[ServiceFilter(typeof(AuthorizeUserTokenAttribute))]
		public async Task<IActionResult> GetVehiclesForUser(string username)
		{
			var res = await ((VehicleDataService)_service).GetVehiclesByUser(username);

			return Ok(res);
		}

		// DELETE: vehicle/delete/{id}
		[HttpDelete]
		[Route("delete/{id}")]
		[ProducesResponseType(200, Type = typeof(bool))]
		[MapToApiVersion("1.0")]
		//[ServiceFilter(typeof(AuthorizeUserTokenAttribute))]
		public async Task<IActionResult> Delete(long id)
		{
			var res = await ((VehicleDataService)_service).Delete(id);

			return Ok(res);
		}


		// GET: vehicle/types
		[HttpGet]
		[Route("types")]
		[ProducesResponseType(200, Type = typeof(List<VehicleTypeDto>))]
		[MapToApiVersion("1.0")]
		//[ServiceFilter(typeof(AuthorizeUserTokenAttribute))]
		public async Task<IActionResult> GetVehiclesTypes()
		{
			var res = await ((VehicleDataService)_service).GetVehicleTypes();

			return Ok(res);
		}

		// GET: vehicle/brands
		[HttpGet]
		[Route("brands")]
		[ProducesResponseType(200, Type = typeof(List<VehicleBrandDto>))]
		[MapToApiVersion("1.0")]
		//[ServiceFilter(typeof(AuthorizeUserTokenAttribute))]
		public async Task<IActionResult> GetVehiclesBrands()
		{
			var res = await ((VehicleDataService)_service).GetVehicleBrands();

			return Ok(res);
		}

		// GET: vehicle/models/{brandid}
		[HttpGet]
		[Route("models/{brandid}")]
		[ProducesResponseType(200, Type = typeof(List<VehicleModelDto>))]
		[MapToApiVersion("1.0")]
		//[ServiceFilter(typeof(AuthorizeUserTokenAttribute))]
		public async Task<IActionResult> GetVehiclesModels(long brandid)
		{
			var res = await ((VehicleDataService)_service).GetVehicleModels(brandid);

			return Ok(res);
		}
	}
}