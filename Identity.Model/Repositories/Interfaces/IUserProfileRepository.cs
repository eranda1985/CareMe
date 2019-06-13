using Identity.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Model.Repositories.Interfaces
{
    public interface IUserProfileRepository: IRepository<UserProfileModel>
    {
        Task<UserProfileModel> GetUserProfile(string username);
        Task<bool> AddOrUpdateProfile(string username, string first, string last, string mobile);
    }
}
