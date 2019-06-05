using NPoco;
using RunningData.Model.DataConnections;
using RunningData.Model.Models;
using RunningData.Model.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RunningData.Model.Repositories
{
	public class FuelDataRepository : DBRepository<FuelDataModel>, IFuelDataRepository<FuelDataModel>
	{
		public Database DBContext { get; private set; }

		public FuelDataRepository(IDataConnection connection)
		{
			DBContext = new Database(connection.ConnectionString, connection.DatabaseType, connection.DbProviderFactory);
			InternalContext = DBContext;
		}

		public async Task<bool> AddFuelData(FuelDataModel poco)
		{
			var res = await Add(poco);
			return (res > -1);
		}

		public void Dispose()
		{
			if (DBContext != null)
			{
				DBContext.Dispose();
			}
		}

		public List<FuelDataModel> GetAll()
		{
			throw new NotImplementedException();
		}
	}
}
