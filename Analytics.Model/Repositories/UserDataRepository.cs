using Analytics.Model.DataConnections;
using Analytics.Model.Models;
using Analytics.Model.Repositories.Interfaces;
using NPoco;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Model.Repositories
{
	public class UserDataRepository : DBRepository<UserDataModel>, IUserDataRepository<UserDataModel>, IDisposable
	{
		public Database DBContext { get; private set; }

		public UserDataRepository(IDataConnection dataConnection)
		{
			DBContext = new Database(dataConnection.ConnectionString, dataConnection.DatabaseType, dataConnection.DbProviderFactory);
			InternalContext = DBContext;
		}

		public async Task<bool> Upsert(UserDataModel poco)
		{
			var user = await GetUserByName(poco.UserName);
			var res = (user != null) ? await Update(poco) : await Add(poco);
			return res > -1;
		}

		public async Task<UserDataModel> GetUserByName(string name)
		{
			var user = await Query("Select * from UserData WHERE UserName=@0", name);
			return user?.FirstOrDefault();
		}

		public void SetDBCotext(Database ctx)
		{
			this.DBContext = ctx;
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
