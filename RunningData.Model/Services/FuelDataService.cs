using System;
using RunningData.Model.Dto;

namespace RunningData.Model.Services
{
    public class FuelDataService: IService<FuelDataDto>
    {
        public FuelDataService(IExceptionService _exceptionService)
        {
        }
    }
}
