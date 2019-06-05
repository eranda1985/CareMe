using Analytics.Core.Validators;
using Analytics.Model.Dto;
using Analytics.Model.Models;
using Analytics.Model.Repositories;
using Analytics.Model.Repositories.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Analytics.Model.Services
{
	public class FuelDataService : IService<FuelDetailsDto>
	{
		private readonly IExceptionService _exceptionService;
		private readonly IFuelDataRepository<FuelDetailsModel> _fuelRepository;
		IService<VehiclesDetailsDto> _vehicleService;

		public FuelDataService(
			IExceptionService exceptionService,
			IFuelDataRepository<FuelDetailsModel> fuelRepository,
			IService<VehiclesDetailsDto> vehicleService)
		{
			_exceptionService = exceptionService;
			_fuelRepository = fuelRepository;
			_vehicleService = vehicleService;
		}

		public async Task<bool> AddEntry(FuelDetailsDto dto)
		{
			_exceptionService.Throw(() => Validator.CheckNull(dto));
			_exceptionService.Throw(() => Validator.CheckNull(((FuelDataRepository)_fuelRepository).DBContext));

			var model = Mapper.Map<FuelDetailsModel>(dto);
			bool result = false;

			using (_fuelRepository)
			{
				var res = await _fuelRepository.AddNew(model);
				result = res > -1;
			}

			if (result == true)
			{
				var updatedVehicleDto = new VehiclesDetailsDto
				{
					VehicleId = dto.VehicleId,
					LastODOMeter = dto.Mileage,
					LastUpdated = dto.Date
				};

				result = await ((VehicleDataService)_vehicleService).UpdateEntry(updatedVehicleDto);
			}

			return result;
		}

		public async Task<bool> DeleteEntry(long id)
		{
			_exceptionService.Throw(() => Validator.CheckNull(((FuelDataRepository)_fuelRepository).DBContext));

			bool result = false;

			using (_fuelRepository)
			{
				var existingPoco = await _fuelRepository.GetEntryById(id);
				result = await _fuelRepository.DeleteEntry(existingPoco);
			}

			return result;
		}

		public async Task<List<FuelDetailsDto>> GetRecentEntries()
		{
			_exceptionService.Throw(() => Validator.CheckNull(((FuelDataRepository)_fuelRepository).DBContext));

			List<FuelDetailsDto> result;

			using (_fuelRepository)
			{
				var modelsList = await _fuelRepository.GetRecentFuelEntries();
				result = Mapper.Map<List<FuelDetailsDto>>(modelsList);
			}

			return result;
		}

		public async Task<List<FuelDetailsDto>> GetBackwardEntries(DateTime seed)
		{
			_exceptionService.Throw(() => Validator.CheckNull(((FuelDataRepository)_fuelRepository).DBContext));

			List<FuelDetailsDto> result;

			using (_fuelRepository)
			{
				var modelsList = await _fuelRepository.GetBackwardEntriesFromOffset(seed);
				result = Mapper.Map<List<FuelDetailsDto>>(modelsList);
			}

			return result;
		}

		public async Task<List<FuelDetailsDto>> GetForwardEntries(DateTime seed)
		{
			_exceptionService.Throw(() => Validator.CheckNull(((FuelDataRepository)_fuelRepository).DBContext));

			List<FuelDetailsDto> result;

			using (_fuelRepository)
			{
				var modelsList = await _fuelRepository.GetForewardEntriesFromOffset(seed);
				result = Mapper.Map<List<FuelDetailsDto>>(modelsList);
			}

			return result;
		}
	}
}
