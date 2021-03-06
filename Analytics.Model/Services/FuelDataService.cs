﻿using Analytics.Core.Validators;
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
		private readonly IService<VehiclesDetailsDto> _vehicleService;

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
			bool insertStatus = false;

			var updatedVehicleDto = new VehiclesDetailsDto
			{
				VehicleId = dto.VehicleId,
				LastODOMeter = dto.Mileage,
				LastUpdated = dto.Date
			};

			var updateStatus = await ((VehicleDataService)_vehicleService).UpdateEntry(updatedVehicleDto);

			if (updateStatus)
			{
				using (_fuelRepository)
				{
					var res = await _fuelRepository.AddNew(model);
					insertStatus = res > -1;
				}
			}

			return (insertStatus && updateStatus);
		}

		public async Task<bool> DeleteEntry(long vehicleId)
		{
			_exceptionService.Throw(() => Validator.CheckNull(((FuelDataRepository)_fuelRepository).DBContext));

			bool result = false;

			using (_fuelRepository)
			{
				var existingPoco = await _fuelRepository.GetEntryById(vehicleId);
				result = await _fuelRepository.DeleteEntry(existingPoco);
			}

			return result;
		}

		public async Task<List<FuelDetailsDto>> GetRecentEntries(long vid)
		{
			_exceptionService.Throw(() => Validator.CheckNull(((FuelDataRepository)_fuelRepository).DBContext));

			List<FuelDetailsDto> result;

			using (_fuelRepository)
			{
				var modelsList = await _fuelRepository.GetRecentFuelEntries(vid);
				result = Mapper.Map<List<FuelDetailsDto>>(modelsList);
			}

			return result;
		}

		public async Task<List<FuelDetailsDto>> GetBackwardEntries(DateTime seed, long vid)
		{
			_exceptionService.Throw(() => Validator.CheckNull(((FuelDataRepository)_fuelRepository).DBContext));

			List<FuelDetailsDto> result;

			using (_fuelRepository)
			{
				var modelsList = await _fuelRepository.GetBackwardEntriesFromOffset(seed, vid);
				result = Mapper.Map<List<FuelDetailsDto>>(modelsList);
			}

			return result;
		}

		public async Task<List<FuelDetailsDto>> GetForwardEntries(DateTime seed, long vid)
		{
			_exceptionService.Throw(() => Validator.CheckNull(((FuelDataRepository)_fuelRepository).DBContext));

			List<FuelDetailsDto> result;

			using (_fuelRepository)
			{
				var modelsList = await _fuelRepository.GetForewardEntriesFromOffset(seed, vid);
				result = Mapper.Map<List<FuelDetailsDto>>(modelsList);
			}

			return result;
		}

		public async Task<List<FuelDetailsDto>> GetFuelDataWithinRange(DateTime upper, DateTime lower, long vid)
		{
			_exceptionService.Throw(() => Validator.CheckNull(((FuelDataRepository)_fuelRepository).DBContext));

			List<FuelDetailsDto> result;

			using (_fuelRepository)
			{
				var modelsList = await _fuelRepository.GetFuelDataWithinRange(upper, lower, vid);
				result = Mapper.Map<List<FuelDetailsDto>>(modelsList);
			}

			return result;
		}
	}
}
