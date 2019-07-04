using Analytics.Model.Models;
using NPoco;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Model.Repositories.Interfaces
{
	public interface IUserDataRepository<U> : IDisposable where U : UserDataModel
	{
		Task<bool> Upsert(U poco);
		Task<UserDataModel> GetUserByName(string username);
		void SetDBCotext(Database ctx);
	}
}
