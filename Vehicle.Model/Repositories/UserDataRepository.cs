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
    public class UserDataRepository : DBRepository<UserDataModel>, IUserDataRepository
    {
        public UserDataRepository(IDataConnection dataConnection) 
            : base(dataConnection.ConnectionString, dataConnection.DatabaseType, dataConnection.DbProviderFactory)
        {
        }

        public async Task<bool> UpsertUser(UserDataModel poco)
        {
            var user = await GetUserByName(poco.UserName);
            var res = (user == null) ? await Add(poco) : await Update(poco);
            return res > -1;
        }

        public async Task<UserDataModel> GetUserByName(string username)
        {
            var user = await Query("Select * from UserData WHERE UserName=@0", username);
            return user?.FirstOrDefault();
        }


    }
}
