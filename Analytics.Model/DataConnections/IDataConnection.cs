using NPoco;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Analytics.Model.DataConnections
{
	public interface IDataConnection
	{
		string ConnectionString { get; }
		DatabaseType DatabaseType { get; }
		DbProviderFactory DbProviderFactory { get; }
	}
}
