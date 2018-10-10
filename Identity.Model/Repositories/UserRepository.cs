using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Model.DataConnections;
using Identity.Model.Models;
using Identity.Model.Repositories.Interfaces;

namespace Identity.Model.Repositories
{
    public class UserRepository : DBRepository<UserModel>, IUserRepository
    {
        public UserRepository(IDataConnection dataConn)
            : base(dataConn.ConnectionString, dataConn.DatabaseType, dataConn.DbProviderFactory)
        {
        }

        public async Task<long> AddUserAsync(UserModel user)
        {
            return await Add(user);
        }

        public List<UserModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<UserModel> GetUserByNameAsync(string name)
        {
            //string sql = @"SELECT * FROM UserDetail where username = @0";
            //var result = await Query(sql, name);
            //return result.FirstOrDefault();
            return GetMockUserByName(name);
        }

        public async Task<UserModel> GetUserByIdAsync(long id)
        {
            //string sql = @"SELECT * FROM UserDetail where Id = @0";
            //var result = await Query(sql, id);
            //return result.FirstOrDefault();
            return GetMockUserByName("eranda1985@yahoo.com");
        }

        public async Task<long> UpdateUserAsync(UserModel user)
        {
            //return await Update(user);
            return GetMockUserByName("eranda1985@yahoo.com").Id;
        }

        private UserModel GetMockUserByName(string name)
        {
            return name == "eranda1985@yahoo.com" ? new UserModel
            {
                Id = 1L,
                DeviceType = "iOS",
                SecretKey = "secret",
                Username = name,
                Password = "test123"
            } : null;
        }
    }
}
