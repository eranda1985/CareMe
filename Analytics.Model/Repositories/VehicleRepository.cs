using Analytics.Model.DataConnections;
using Analytics.Model.Models;
using Analytics.Model.Repositories.Interfaces;
using NPoco;
using System.Linq;
using System.Threading.Tasks;

namespace Analytics.Model.Repositories
{
	public class VehicleRepository : DBRepository<VehicleDetailsModel>, IVehicleRepository<VehicleDetailsModel>
	{
		public Database DBContext { get; private set; }

		public VehicleRepository(IDataConnection dataConnection)
		{
			DBContext = new Database(dataConnection.ConnectionString, dataConnection.DatabaseType, dataConnection.DbProviderFactory);
			InternalContext = DBContext;
		}

		public async Task<long> AddNew(VehicleDetailsModel poco)
		{
			var res = await Add(poco);
			return res;
		}

		public async Task<bool> DeleteEntry(VehicleDetailsModel poco)
		{
			var res = await Delete(poco);
			return res > -1;
		}

		public void Dispose()
		{
			if (this.DBContext != null)
			{
				DBContext.Dispose();
			}
		}

		public void SetDBContext(Database context)
		{
			this.DBContext = context;
		}

		public async Task<VehicleDetailsModel> GetVehicleById(long vehicleId)
		{
			var qstring = "SELECT * FROM VehicleDetails WHERE VehicleId = @0";
			var res = await Query(qstring, vehicleId);
			return res.FirstOrDefault();
		}

		public async Task<bool> UpdateVehicle(VehicleDetailsModel poco)
		{
			var res = await Update(poco);
			return res > -1;
		}
	}
}
