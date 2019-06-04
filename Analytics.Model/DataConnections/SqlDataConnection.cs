using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using Analytics.Core;
using Microsoft.Extensions.Configuration;
using NPoco;

namespace Analytics.Model.DataConnections
{
	public class SqlDataConnection : IDataConnection
	{
		private AppSettings _appSettings;

		public SqlDataConnection(IConfiguration configuration)
		{
			_appSettings = configuration.Get<AppSettings>();
		}

		public string ConnectionString => _appSettings.ConnectionStrings[AppConstants.CareMeAnalyticsDB];

		public DatabaseType DatabaseType => new NPoco.DatabaseTypes.SqlServerDatabaseType();

		public DbProviderFactory DbProviderFactory => SqlClientFactory.Instance;
	}
}
