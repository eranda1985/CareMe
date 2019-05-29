﻿using System;
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
    }
}
