using NPoco;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Model.Models;

namespace Vehicle.Model.Repositories.Interfaces
{
    public interface IVehicleUserDataRepository: IRepository<VehicleUserDataModel>
    {
        Task<long> AddNew(VehicleUserDataModel poco);
        Task<List<VehicleUserDataModel>> GetExistingUserVehicles(string username);
        Task<long> UpdateEntry(VehicleUserDataModel poco);
		Task<long> DeleteEntry(VehicleUserDataModel poco);
		Task<VehicleUserDataModel> GetVehicleUserById(long id);
	}
}
