using System;
using System.Threading.Tasks;
using RunningData.Model.Dto;

namespace RunningData.Model.Services
{
    public class FuelDataService: IService<FuelDataDto>
    {
        public FuelDataService(IExceptionService _exceptionService)
        {
        }

        public async Task<bool> InsertAsync(params object[] args)
        {
            
        }
    }
}
