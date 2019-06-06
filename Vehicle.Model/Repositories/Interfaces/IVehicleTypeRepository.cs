using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Model.Models;

namespace Vehicle.Model.Repositories.Interfaces
{
	public interface IVehicleTypeRepository: IRepository<VehicleTypeModel>
	{
		Task<List<VehicleTypeModel>> GetVehicleTypes();
	}
}
