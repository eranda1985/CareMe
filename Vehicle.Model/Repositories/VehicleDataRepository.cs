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

        public async Task<bool> AddVNewVehicle(VehicleDataModel poco)
        {
            var res = await Add(poco);
            return res > -1;
        }
    }
}
