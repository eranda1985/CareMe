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
	public class VehicleModelRepository : DBRepository<VehicleModelDataModel>, IVehicleModelRepository
	{
		public VehicleModelRepository(IDataConnection dataConnection) 
			: base(dataConnection.ConnectionString, dataConnection.DatabaseType, dataConnection.DbProviderFactory)
		{
		}

		public async Task<List<VehicleModelDataModel>> GetVehicleModels(long brandId)
		{
			using (DbContext)
			{
				return await Query("SELECT * FROM VehicleModels WHERE BrandId = @0", brandId);
			}
		}
	}
}
