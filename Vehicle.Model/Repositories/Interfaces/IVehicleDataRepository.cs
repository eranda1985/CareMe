using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Model.Models;

namespace Vehicle.Model.Repositories.Interfaces
{
	public interface IVehicleDataRepository : IRepository<VehicleDataModel>
	{
		Task<bool> AddNewVehicle(VehicleDataModel poco, string username);
		Task<VehicleDataModel> GetVehicleByRego(string rego);
		Task<VehicleDataModel> GetVehicleById(long id);
		Task<long> UpdateEntry(VehicleDataModel poco);
		Task<long> DeleteEntry(VehicleDataModel poco);
		Task<List<VehicleDataModel>> GetVehiclesByUsername(string username);
	}
}
