﻿using System;
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

        public async Task<UserModel> AddUserAsync(UserModel user)
        {
            return await Add(user);
        }

        public List<UserModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<UserModel> GetUserByNameAsync(string name)
        {
            string sql = @"SELECT * FROM UserDetail where username = @0";
            var result = await Query(sql, name);
            return result.FirstOrDefault();
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
