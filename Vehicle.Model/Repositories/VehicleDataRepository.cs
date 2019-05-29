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
    public class VehicleDataRepository : DBRepository<VehicleDataModel>, IVehicleDataRepository
    {
        IVehicleUserDataRepository _vehicleUserDataRepository;

        public VehicleDataRepository(IDataConnection dataConnection, IVehicleUserDataRepository vehicleUserDataRepository)
            : base(dataConnection.ConnectionString, dataConnection.DatabaseType, dataConnection.DbProviderFactory)
        {
            _vehicleUserDataRepository = vehicleUserDataRepository;
        }

        public async Task<bool> AddNewVehicle(VehicleDataModel poco, string userId)
        {
            bool result = false;

            var prop = _vehicleUserDataRepository.GetType().GetProperty("DBContext");

            prop?.SetValue(_vehicleUserDataRepository, this.DbContext);

            using (DbContext)
            {
                using (var trans = DbContext.GetTransaction())
                {
                    var vehicleId = await Add(poco);

                    var vehicleUserModel = new VehicleUserDataModel
                    {
                        Username = userId,
                        VehicleId = vehicleId,
                        IsDefault = false
                    };

                    var vuserId = await _vehicleUserDataRepository.AddNew(vehicleUserModel);
                    result = (vehicleId > -1) && (vuserId > -1);
                    trans.Complete();
                }
            }
            return result;
        }

        public async Task<VehicleDataModel> GetVehicleByRego(string rego)
        {
            using (DbContext)
            {
                var res = await Query("SELECT * FROM VehicleData WHERE RegoPlate=@0", rego);
                return res.FirstOrDefault();
            }
        }
    }
}
