using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Vehicle.Core.Validators;
using Vehicle.Model.Dto;
using Vehicle.Model.Models;
using Vehicle.Model.Repositories.Interfaces;

namespace Vehicle.Model.Services
{
	public class VehicleDataSubscriberService
	{
		private readonly IExceptionService _exceptionService;
		private readonly IVehicleDataRepository _vehicleDataRepository;
		private readonly ILogger<VehicleDataService> _logger;

		public VehicleDataSubscriberService(
			ILogger<VehicleDataService> logger,
				IExceptionService exceptionService,
				IVehicleDataRepository vehicleDataRepository)
		{
			_logger = logger;
			_exceptionService = exceptionService;
			_vehicleDataRepository = vehicleDataRepository;
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
		/// Retrives a vehicle by id
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		public async Task<VehicleDataDto> GetVehicleById(params object[] args)
		{
			_exceptionService.Throw(() => Validator.CheckArgsLength(args, 1));

			var id = (long)args[0];
			var poco = await _vehicleDataRepository.GetVehicleById(id);
			var dto = Mapper.Map<VehicleDataDto>(poco);
			return dto;
		}
	}
}
