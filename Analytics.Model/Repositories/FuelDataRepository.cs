using Analytics.Model.DataConnections;
using Analytics.Model.Models;
using Analytics.Model.Repositories.Interfaces;
using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analytics.Model.Repositories
{
	public class FuelDataRepository : DBRepository<FuelDetailsModel>, IFuelDataRepository<FuelDetailsModel>
	{
		public Database DBContext { get; private set; }

		public FuelDataRepository(IDataConnection connection)
		{
			DBContext = new Database(connection.ConnectionString, connection.DatabaseType, connection.DbProviderFactory);
			InternalContext = DBContext;
		}

		public async Task<long> AddNew(FuelDetailsModel poco)
		{
			var res = await Add(poco);
			return res;
		}

		public async Task<bool> DeleteEntry(FuelDetailsModel poco)
		{
			var res = await Delete(poco);
			return res > -1;
		}

		public void Dispose()
		{
			if (DBContext != null)
			{
				DBContext.Dispose();
			}
		}

		public async Task<List<FuelDetailsModel>> GetRecentFuelEntries(long vid)
		{
			var qstring = "SELECT TOP (7) * FROM FuelConsumption WHERE VehicleId = @0 ORDER BY [Date] DESC";
			return await Query(qstring, vid);
		}

		public async Task<List<FuelDetailsModel>> GetBackwardEntriesFromOffset(DateTime seed)
		{
			var qstring = "SELECT TOP (7) * FROM FuelConsumption WHERE [Date] < @0 ORDER BY [Date] DESC";
			return await Query(qstring);
		}

		public void SetDBContext(Database context)
		{
			DBContext = context;
		}

		public async Task<List<FuelDetailsModel>> GetForewardEntriesFromOffset(DateTime seed)
		{
			var qstring = "SELECT TOP (7) * FROM FuelConsumption WHERE [Date] > @0 ORDER BY [Date] DESC";
			return await Query(qstring);
		}

		public async Task<FuelDetailsModel> GetEntryById(long vid)
		{
			var qstring = "SELECT * FROM FuelConsumption WHERE VehicleId=@0";
			var res = await Query(qstring, vid);
			return res.FirstOrDefault();
		}
	}
}
