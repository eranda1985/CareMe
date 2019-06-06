using System;
using System.Threading.Tasks;
using RunningData.Model.Models;

namespace RunningData.Model.Repositories.Interfaces
{
    public interface IFuelDataRepository<T>: IDisposable where T: FuelDataModel
    {
        Task<bool> AddFuelData(T poco);
    }
}
