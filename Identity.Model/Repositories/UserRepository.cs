using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.Model.Models;
using Identity.Model.Repositories.Interfaces;

namespace Identity.Model.Repositories
{
    public class UserRepository : IUserRepository
    {
        public async Task<UserModel> AddUserAsync(UserModel user)
        {
            return user;
        }

        public List<UserModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<UserModel> GetUserByNameAsync(string name)
        {
            return GetMockUserByName(name);
        }

        private UserModel GetMockUserByName(string name)
        {
            return name == "test@123.com" ? new UserModel
            {
                Username = "test@123.com",
                Password = "testPassword"
            } : null;
        }
    }
}
