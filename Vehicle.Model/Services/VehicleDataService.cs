using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Core.Exceptions;
using Vehicle.Core.Validators;
using Vehicle.Model.Dto;
using Vehicle.Model.Models;
using Vehicle.Model.Repositories.Interfaces;

namespace Vehicle.Model.Services
{
	public class VehicleDataService : IService<VehicleDataDto>
	{
		private IExceptionService _exceptionService;
		private IVehicleDataRepository _vehicleDataRepository;
		private IVehicleUserDataRepository _vehicleUserDataRepository;
		private ILogger<VehicleDataService> _logger;
		private IService<UserDataDto> _userService;

		public VehicleDataService(ILogger<VehicleDataService> logger,
				IExceptionService exceptionService,
				IVehicleDataRepository vehicleDataRepository,
				IService<UserDataDto> userService,
				IVehicleUserDataRepository vehicleUserDataRepository)
		{
			_logger = logger;
			_exceptionService = exceptionService;
			_vehicleDataRepository = vehicleDataRepository;
			_userService = userService;
			_vehicleUserDataRepository = vehicleUserDataRepository;
		}

		/// <summary>
		/// Add vehicle
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		public async Task<VehicleDataDto> AddVehicle(params object[] args)
		{
			_logger.LogDebug("Calling AddVehicle for {0}", args[4]);

			_exceptionService.Throw(() => Validator.CheckArgsLength(args, 8));
			_exceptionService.Throw(() => Validator.CheckType<DateTime>(args[5]));

			var vehicleType = args[0] as string;
			var brand = args[1] as string;
			var model = args[2] as string;
			var fuelType = args[3] as string;
			var regoPlate = args[4] as string;
			var date = (DateTime)args[5];
			var odoMeter = (double)args[6];
			var username = args[7] as string;

			var dto = new VehicleDataDto
			{
				VehicleType = vehicleType,
				Brand = brand,
				Model = model,
				FuelType = fuelType,
				RegoPlate = regoPlate,
				Date = date,
				ODOMeter = odoMeter
			};

			var userDto = await ((UserdataService)_userService).GetUserByName(username);
			var vehicle = await _vehicleDataRepository.GetVehicleByRego(dto.RegoPlate);

			_exceptionService.Throw(() => Validator.CheckNull(userDto));
			_exceptionService.Throw(() =>
			{
				if (vehicle != null)
				{
					throw new ValidationException("A Vehicle already exists with the supplied rego.");
				}
			});

			var vehicledataPoco = Mapper.Map<VehicleDataModel>(dto);
			var res = await _vehicleDataRepository.AddNewVehicle(vehicledataPoco, username) && await SetDefaultVehicle(dto.RegoPlate, username);
			return (res) ? dto : null;
		}

		/// <summary>
		/// Set default vehicle
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		public async Task<bool> SetDefaultVehicle(params object[] args)
		{
			_logger.LogDebug("Calling SetDefaultVehicle");
			_exceptionService.Throw(() => Validator.CheckArgsLength(args, 2));

			bool result = false;

			var vehicleRego = args[0] as string;
			var username = args[1] as string;

			var vehicle = await _vehicleDataRepository.GetVehicleByRego(vehicleRego);
			var list = await _vehicleUserDataRepository.GetExistingUserVehicles(username);
			foreach (var item in list)
			{
				if (item.VehicleId == vehicle.Id)
				{
					item.IsDefault = true;
				}
				else
				{
					item.IsDefault = false;
				}

				var res = await _vehicleUserDataRepository.UpdateEntry(item);
				result = res > -1;
			}

			return result;
		}

		/// <summary>
		/// Update Vehicle details.
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		public async Task<bool> Update(params object[] args)
		{
			_exceptionService.Throw(() => Validator.CheckArgsLength(args, 1));
			_exceptionService.Throw(() => Validator.CheckType<VehicleDataDto>(args[0]));

			var dto = args[0] as VehicleDataDto;
			var model = Mapper.Map<VehicleDataModel>(dto);

			var res = await _vehicleDataRepository.UpdateEntry(model);
			return res > -1;

		}

		/// <summary>
		/// Get vehicles for user
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		public async Task<List<VehicleDataDto>> GetVehiclesByUser(params object[] args)
		{
			_exceptionService.Throw(() => Validator.CheckArgsLength(args, 1));
			_exceptionService.Throw(() => Validator.CheckType<string>(args[0]));

			var username = args[0] as string;

			var list = await _vehicleDataRepository.GetVehiclesByUsername(username);
			var vuserList = await _vehicleUserDataRepository.GetExistingUserVehicles(username);

			var dtoList = Mapper.Map<List<VehicleDataDto>>(list, opt => opt.AfterMap((src, dest) =>
				{
					foreach (var item in ((List<VehicleDataDto>)dest))
					{
						item.IsSelected = vuserList.Where(x => item.Id == x.VehicleId).FirstOrDefault().IsDefault;
					}
				}));

			return dtoList;
		}

		/// <summary>
		/// Delete 
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		public async Task<bool> Delete(params object[] args)
		{
			_exceptionService.Throw(() => Validator.CheckArgsLength(args, 1));

			var id = (long)args[0];
			var poco = await _vehicleDataRepository.GetVehicleById(id);
			var res = await _vehicleDataRepository.DeleteEntry(poco);
			
			return res > -1;

		}

		// Get Vehicle types, brands, models, fueltypes
	}
}
