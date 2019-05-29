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
    public class VehicleDataRepository : DBRepository<VehicleDataModel>, IVehicleDataRepository
    {
        public VehicleDataRepository(IDataConnection dataConnection) 
            : base(dataConnection.ConnectionString, dataConnection.DatabaseType, dataConnection.DbProviderFactory)
        {
        }

        public async Task<long> AddVNewVehicle(VehicleDataModel poco, long userId)
        {
			using (var trans = DbContext.GetTransaction())
			{
				var vehicleId = await Add(poco);

				var vehicleUserModel = new VehicleUserDataModel
				{
					UserId = userId,
					VehicleId = vehicleId,
					IsDefault = false
				};

				trans.Complete();
			}
				


			return res;
        }
    }
}
