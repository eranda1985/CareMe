using System;
using System.Threading.Tasks;
using Identity.Model.Models;

namespace Identity.Model.Repositories.Interfaces
{
    public interface IUserRepository: IRepository<UserModel>
    {
        Task<UserModel> GetUserByNameAsync(string name);

        Task<UserModel> AddUserAsync(UserModel user);
    }
}
