using RunningData.Model.Models;
using System;
using System.Threading.Tasks;

namespace RunningData.Model.Repositories.Interfaces
{
	public interface IUserDataRepository<T> : IDisposable where T : UserDataModel
	{
		Task<bool> UpsertUser(T user);
		Task<T> GetUserByName(string username);
	}
}
