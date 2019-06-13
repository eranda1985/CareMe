using Identity.Model.DataConnections;
using Identity.Model.Models;
using Identity.Model.Repositories.Interfaces;
using NPoco;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Model.Repositories
{
    public class UserProfileRepository : DBRepository<UserProfileModel>, IUserProfileRepository
    {
        public UserProfileRepository(IDataConnection dataConn) 
            : base(dataConn.ConnectionString, dataConn.DatabaseType, dataConn.DbProviderFactory)
        {
        }

        public async Task<bool> AddOrUpdateProfile(string username, string first, string last, string mobile)
        {
            bool finalResult = false;
            using (DbContext)
            {
                var queryGetExisting = "SELECT * FROM UserProfile WHERE Username = @0";
                var existing = await Query(queryGetExisting, username);
                if(existing != null && existing.Any())
                {
                    // Update
                    var res = await Update(new UserProfileModel
                    {
                        Id = existing.FirstOrDefault().Id,
                        FirstName = first,
                        LastName = last,
                        Username = username,
                        Mobile = mobile
                    });

                    finalResult = res > -1;
                }
                else
                {
                    // Add
                    var res = await Add(new UserProfileModel
                    {
                        FirstName = first,
                        LastName = last,
                        Username = username,
                        Mobile = mobile
                    });

                    finalResult = res > -1;
                }
            }

            return finalResult;
        }

        public List<UserProfileModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<UserProfileModel> GetUserProfile(string username)
        {
            using (DbContext)
            {
                var query = "SELECT * FROM UserProfile WHERE Username = @0";
                var res = await Query(query, username);
                return res.FirstOrDefault();
            }
        }
    }
}
