using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.Model.Models;

namespace Identity.Model.Repositories.Interfaces
{
    public interface IVersionRepository: IRepository<AppVersionModel>
    {
        Task<List<AppVersionModel>> GetAllowedVersion(string deviceType);
    }
}
