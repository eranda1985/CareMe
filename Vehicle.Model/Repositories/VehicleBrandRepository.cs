using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using NPoco;
using Vehicle.Model.DataConnections;
using Vehicle.Model.Models;
using Vehicle.Model.Repositories.Interfaces;

namespace Vehicle.Model.Repositories
{
	public class VehicleBrandRepository : DBRepository<VehicleBrandModel>, IVehicleBrandRepository
	{
		public VehicleBrandRepository(IDataConnection dataConnection) 
			: base(dataConnection.ConnectionString, dataConnection.DatabaseType, dataConnection.DbProviderFactory)
		{
		}

		public async Task<List<VehicleBrandModel>> GetVehicleBrands()
		{
			using (DbContext)
			{
				return await Query("SELECT * FROM VehicleBrands");
			}
		}
	}
}
