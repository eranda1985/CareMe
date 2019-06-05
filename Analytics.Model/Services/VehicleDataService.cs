﻿using Analytics.Core.Validators;
using Analytics.Model.Dto;
using Analytics.Model.Models;
using Analytics.Model.Repositories;
using Analytics.Model.Repositories.Interfaces;
using AutoMapper;
using System.Threading.Tasks;

namespace Analytics.Model.Services
{
	public class VehicleDataService : IService<VehiclesDetailsDto>
	{
		private readonly IExceptionService _exceptionService;
		private readonly IVehicleRepository<VehicleDetailsModel> _vehicleRepository;

		public VehicleDataService(
			IExceptionService exceptionService,
			IVehicleRepository<VehicleDetailsModel> vehicleRepository)
		{
			_exceptionService = exceptionService;
			_vehicleRepository = vehicleRepository;
		}

		public async Task<bool> AddEntry(VehiclesDetailsDto dto)
		{
			_exceptionService.Throw(() => Validator.CheckNull(dto));
			_exceptionService.Throw(() => Validator.CheckNull(((VehicleRepository)_vehicleRepository).DBContext));

			var model = Mapper.Map<VehicleDetailsModel>(dto);
			bool result = false;

			using (_vehicleRepository)
			{
				var res = await _vehicleRepository.AddNew(model);
				result = res > -1;
			}

			return result;
		}

		public async Task<bool> DeleteEntry(long vehicleId)
		{
			_exceptionService.Throw(() => Validator.CheckNull(((VehicleRepository)_vehicleRepository).DBContext));

			bool result = false;

			using (_vehicleRepository)
			{
				var existingPoco = await _vehicleRepository.GetVehicleById(vehicleId);
				result = await _vehicleRepository.DeleteEntry(existingPoco);
			}

			return result;
		}
	}
}
