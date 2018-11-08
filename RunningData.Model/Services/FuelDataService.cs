using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RunningData.Core.Validators;
using RunningData.Model.Dto;
using RunningData.Model.Models;
using RunningData.Model.Repositories.Interfaces;

namespace RunningData.Model.Services
{
    public class FuelDataService: IService<FuelDataDto>
    {
        private IFuelDataRepository _fuelDataRepository;
        private readonly IExceptionService _exceptionService;
        private ILogger<FuelDataService> _logger;
        private IConfiguration _configuration;

        public FuelDataService(ILogger<FuelDataService> logger, 
                                IExceptionService exceptionService, 
                                IFuelDataRepository fuelDataRepository,
                                IConfiguration configuration)
        {
            _logger = logger;
            _exceptionService = exceptionService;
            _fuelDataRepository = fuelDataRepository;
            _configuration = configuration;
        }

        /// <summary>
        /// Adds a new fuel consumption record
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public async Task<bool> InsertAsync(params object[] args)
        {
            _exceptionService.Throw(() => Validator.CheckArgsLength(args, 4));
            _exceptionService.Throw(() => Validator.CheckType<DateTime>(args[0]));
            _exceptionService.Throw(() => Validator.CheckType<double>(args[1]));
            _exceptionService.Throw(() => Validator.CheckType<DateTime>(args[2]));
            _exceptionService.Throw(() => Validator.CheckType<DateTime>(args[3]));

            var date = (DateTime)args[0];
            var litres = (double)args[1];
            var price = (double)args[2];
            var mileage = (double)args[3];

            return await _fuelDataRepository.AddFuelData(new FuelDataModel
            {
                Date = date,
                Litres = litres,
                Price = price,
                Mileage = mileage
            });
        }
    }
}
