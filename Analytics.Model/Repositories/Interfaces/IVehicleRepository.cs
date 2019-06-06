using Analytics.Model.Models;
using NPoco;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Model.Repositories.Interfaces
{
	public interface IVehicleRepository<T>: IDisposable where T: VehicleDetailsModel
	{
		void SetDBContext(Database context);
		Task<long> AddNew(T poco);
		Task<bool> DeleteEntry(T poco);
		Task<VehicleDetailsModel> GetVehicleById(long id);
		Task<bool> UpdateVehicle(T poco);
	}
}
