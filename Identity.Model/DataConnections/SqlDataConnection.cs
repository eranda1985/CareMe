using System;
using System.Data.Common;
using Identity.Core;
using Microsoft.Extensions.Configuration;
using NPoco;

namespace Identity.Model.DataConnections
{
    public class SqlDataConnection : IDataConnection
    {
        public string ConnectionString { get => _appSettings.ConnectionStrings[AppConstants.CareMeDB]; }
        public DatabaseType DatabaseType { get => new NPoco.DatabaseTypes.SqlServerDatabaseType(); }
        public DbProviderFactory DbProviderFactory { get => System.Data.SqlClient.SqlClientFactory.Instance; }

        private AppSettings _appSettings;

        public SqlDataConnection(IConfiguration configuration)
        {
            _appSettings = configuration.Get<AppSettings>();
        } 
    }
}
