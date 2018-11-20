using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Core.Validators;
using Vehicle.Model.Dto;
using Vehicle.Model.Models;
using Vehicle.Model.Repositories.Interfaces;

namespace Vehicle.Model.Services
{
    public class VehicleDataService : IService<VehicleDataDto>
    {
        IExceptionService _exceptionService;
        IVehicleDataRepository _vehicleDataRepository;
        ILogger<VehicleDataService> _logger;

        public VehicleDataService(ILogger<VehicleDataService> logger, 
            IExceptionService exceptionService, 
            IVehicleDataRepository vehicleDataRepository)
        {
            _logger = logger;
            _exceptionService = exceptionService;
            _vehicleDataRepository = vehicleDataRepository;
        }

        public async Task<bool> AddVehicle(params object[] args)
        {
            _logger.LogDebug("Calling AddVehicle for {0}", args[4]);

            _exceptionService.Throw(() => Validator.CheckArgsLength(args, 7));
            _exceptionService.Throw(() => Validator.CheckType<DateTime>(args[5]));

            var vehicleType = args[0] as string;
            var brand = args[1] as string;
            var model = args[2] as string;
            var fuelType = args[3] as string;
            var regoPlate = args[4] as string;
            var date = (DateTime)args[5];
            var odoMeter = (double)args[6];

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

            var poco = Mapper.Map<VehicleDataModel>(dto);

            return await _vehicleDataRepository.AddVNewVehicle(poco);
        }
    }
}
