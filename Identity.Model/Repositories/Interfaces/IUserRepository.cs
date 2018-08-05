using System;
using System.Threading.Tasks;
using Identity.Model.Models;

namespace Identity.Model.Repositories.Interfaces
{
    public interface IUserRepository: IRepository<UserModel>
    {
        Task<UserModel> GetUserByNameAsync(string name);
        Task<long> AddUserAsync(UserModel user);
        Task<UserModel> GetUserByIdAsync(long id);
        Task<long> UpdateUserAsync(UserModel user);
    }
}
