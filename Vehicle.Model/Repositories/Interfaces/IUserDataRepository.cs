using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Model.Models;

namespace Vehicle.Model.Repositories.Interfaces
{
    public interface IUserDataRepository: IRepository<UserDataModel>
    {
        Task<bool> UpsertUser(UserDataModel poco);
        Task<UserDataModel> GetUserByName(string username);
    }
}
