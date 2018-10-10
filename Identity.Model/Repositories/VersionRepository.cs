using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Identity.Model.DataConnections;
using Identity.Model.Models;
using Identity.Model.Repositories.Interfaces;
using NPoco;

namespace Identity.Model.Repositories
{
    public class VersionRepository : DBRepository<AppVersionModel>, IVersionRepository
    {
        public VersionRepository(IDataConnection dataConn) : base(dataConn.ConnectionString, dataConn.DatabaseType, dataConn.DbProviderFactory)
        {
        }

        public List<AppVersionModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<List<AppVersionModel>> GetAllowedVersion(string deviceType)
        {
            return await Task.FromResult(GetMockAllowedVersions(deviceType));
        }

        /// <summary>
        /// Gets mock allowed versions.
        /// </summary>
        /// <returns>The mock allowed versions.</returns>
        List<AppVersionModel> GetMockAllowedVersions(string deviceType)
        {
            return new List<AppVersionModel>{
                new AppVersionModel{
                    Id = 1L,
                    VersionHash = "1234",
                    VersionNumber = "1.0",
                    Enabled = true,
                    DeviceType = deviceType
                }
            };
        }
    }
}
