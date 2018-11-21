using System;
using System.Data.Common;
using NPoco;

namespace Vehicle.Model.DataConnections
{
    public interface IDataConnection
    {
        string ConnectionString { get; }
        DatabaseType DatabaseType { get; }
        DbProviderFactory DbProviderFactory { get; }
    }
}
