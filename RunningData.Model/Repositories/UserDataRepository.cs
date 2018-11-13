using NPoco;
using RunningData.Model.DataConnections;
using RunningData.Model.Models;
using RunningData.Model.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace RunningData.Model.Repositories
{
	public class UserDataRepository : DBRepository<UserDataModel>, IUserDataRepository
	{
		public UserDataRepository(IDataConnection connection) 
			: base(connection.ConnectionString, connection.DatabaseType, connection.DbProviderFactory)
		{
		}

		public async Task<bool> AddUser(UserDataModel user)
		{
			var res = await Add(user);
			return res > -1;
		}

		public List<UserDataModel> GetAll()
		{
			throw new NotImplementedException();
		}
	}
}
