using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RunningData.Model.DataConnections;
using RunningData.Model.Models;
using RunningData.Model.Repositories.Interfaces;

namespace RunningData.Model.Repositories
{
    public class FuelDataRepository : DBRepository<FuelDataModel>, IFuelDataRepository
    {
        public FuelDataRepository(IDataConnection connection):
        base(connection.ConnectionString, connection.DatabaseType, connection.DbProviderFactory)
        {

        }

        public async Task<bool> AddFuelData(FuelDataModel poco)
        {
            //var res = await Add(poco);
            //return (res > -1);
            return true;
        }

        public List<FuelDataModel> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
