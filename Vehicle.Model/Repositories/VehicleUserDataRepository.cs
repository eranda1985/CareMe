using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco;
using Vehicle.Model.DataConnections;
using Vehicle.Model.Models;
using Vehicle.Model.Repositories.Interfaces;

namespace Vehicle.Model.Repositories
{
    public class VehicleUserDataRepository : DBRepository<VehicleUserDataModel>, IVehicleUserDataRepository
    {
        public Database DBContext { get; set; }

        public VehicleUserDataRepository(IDataConnection dataConnection)
            : base(dataConnection.ConnectionString, dataConnection.DatabaseType, dataConnection.DbProviderFactory)
        {
        }

        public async Task<long> AddNew(VehicleUserDataModel poco)
        {
            if(DBContext != null)
            {
                this.DbContext = DBContext;
            }

            var res = await Add(poco);
            return res; 
        }

        public async Task<List<VehicleUserDataModel>> GetExistingUserVehicles(string username)
        {
            using (DbContext)
            {
                return await Query("SELECT * FROM VehicleUsers WHERE Username=@0", username);
            }
        }

        public async Task<long> UpdateEntry(VehicleUserDataModel poco)
        {
            using (DbContext)
            {
                return await Update(poco);
            }
        }

		public async Task<long> DeleteEntry(VehicleUserDataModel poco)
		{
			if (DBContext != null)
			{
				this.DbContext = DBContext;
			}
			
			return await Delete(poco);
		}

		public async Task<VehicleUserDataModel> GetVehicleUserById(long id)
		{
			using (DbContext)
			{
				var res = await Query("SELECT * FROM VehicleUsers WHERE VehicleId=@0", id);
				return res.FirstOrDefault();
			}
		}
	}
}
