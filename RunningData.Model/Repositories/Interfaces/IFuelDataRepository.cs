using System;
using System.Threading.Tasks;
using RunningData.Model.Models;

namespace RunningData.Model.Repositories.Interfaces
{
    public interface IFuelDataRepository: IRepository<FuelDataModel>
    {
        Task<bool> AddFuelData(FuelDataModel poco);
    }
}
