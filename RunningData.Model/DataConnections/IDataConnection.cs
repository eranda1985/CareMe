using System;
using System.Data.Common;
using NPoco;

namespace RunningData.Model.DataConnections
{
    public interface IDataConnection
    {
        string ConnectionString { get; }
        DatabaseType DatabaseType { get; }
        DbProviderFactory DbProviderFactory { get; }
    }
}
