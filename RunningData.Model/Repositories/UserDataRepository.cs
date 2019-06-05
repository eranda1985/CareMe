using NPoco;
using RunningData.Model.DataConnections;
using RunningData.Model.Models;
using RunningData.Model.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningData.Model.Repositories
{
	public class UserDataRepository : DBRepository<UserDataModel>, IUserDataRepository<UserDataModel>
	{
		public Database DBContext { get; private set; }

		public UserDataRepository(IDataConnection connection)
		{
			DBContext = new Database(connection.ConnectionString, connection.DatabaseType, connection.DbProviderFactory);
			InternalContext = DBContext;
		}

		public async Task<bool> UpsertUser(UserDataModel user)
		{
			var userExisting = await GetUserByName(user.UserName);
			var res = (userExisting == null) ? await Add(user) : await Update(user);
			return res > -1;
		}

		public async Task<UserDataModel> GetUserByName(string username)
		{
			var user = await Query("Select * from UserData WHERE UserName=@0", username);
			return user?.FirstOrDefault();
		}

		public void Dispose()
		{
			if (DBContext != null)
			{
				DBContext.Dispose();
			}
		}
	}
}
