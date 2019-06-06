using CareMe.IntegrationService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RunningData.Core.Validators;
using RunningData.Model.Dto;
using RunningData.Model.Models;
using RunningData.Model.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace RunningData.Model.Services
{
	public class FuelDataService : IService<FuelDataDto>
	{
		private readonly IFuelDataRepository<FuelDataModel> _fuelDataRepository;
		private readonly IExceptionService _exceptionService;
		private readonly ILogger<FuelDataService> _logger;
		private readonly IConfiguration _configuration;
		private readonly IServiceBus _serviceBus;

		public FuelDataService(ILogger<FuelDataService> logger,
														IExceptionService exceptionService,
														IFuelDataRepository<FuelDataModel> fuelDataRepository,
														IConfiguration configuration,
														IServiceBus serviceBus)
		{
			_logger = logger;
			_exceptionService = exceptionService;
			_fuelDataRepository = fuelDataRepository;
			_configuration = configuration;
			_serviceBus = serviceBus;
		}

		/// <summary>
		/// Adds a new fuel consumption record
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		public async Task<bool> InsertAsync(params object[] args)
		{
			_exceptionService.Throw(() => Validator.CheckArgsLength(args, 5));
			_exceptionService.Throw(() => Validator.CheckType<DateTime>(args[0]));
			_exceptionService.Throw(() => Validator.CheckType<double>(args[1]));
			_exceptionService.Throw(() => Validator.CheckType<double>(args[2]));
			_exceptionService.Throw(() => Validator.CheckType<double>(args[3]));

			var date = (DateTime)args[0];
			var litres = (double)args[1];
			var price = (double)args[2];
			var mileage = (double)args[3];
			var vid = (long)args[4];
			bool result = false;

			result = await _fuelDataRepository.AddFuelData(new FuelDataModel
			{
				Date = date,
				Litres = litres,
				Price = price,
				Mileage = mileage,
				VehicleId = vid
			});

			if (result)
			{
				// Notify other APIs about the fuel record. 
				_serviceBus.Publish(new FuelRecordAddedEvent
				{
					VehicleId = vid,
					Price = price,
					Mileage = mileage,
					Litres = litres,
					Date = date.ToString("yyyy/MM/dd")
				});

			}

			return result;
		}
	}
}
