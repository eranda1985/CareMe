using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using NPoco;

namespace Vehicle.Model.Repositories
{
    public class DBRepository<T> : IDisposable where T : class
    {
        protected Database DbContext => getDataContext();
        private string _connString;
        private DatabaseType _databaseType;
        private DbProviderFactory _dbProviderFactory;

        public DBRepository(string connectionString, DatabaseType databaseType, DbProviderFactory dbProvideerFactory)
        {
            _connString = connectionString;
            _databaseType = databaseType;
            _dbProviderFactory = dbProvideerFactory;
        }

        private Database getDataContext()
        {
            return new Database(_connString, _databaseType, _dbProviderFactory);
        }

        /// <summary>
        /// Query the specified query and args.
        /// </summary>
        /// <returns>The query.</returns>
        /// <param name="query">Query.</param>
        /// <param name="args">Arguments.</param>
        protected async Task<List<T>> Query(string query, params object[] args)
        {
            Sql q = new Sql(query, args);
            var result = await DbContext.FetchAsync<T>(q);
            return result;
        }

        /// <summary>
        /// Add the specified item.
        /// </summary>
        /// <returns>The add.</returns>
        /// <param name="item">Item.</param>
        protected async Task<long> Add(T item)
        {
            var id = await DbContext.InsertAsync<T>(item);
            var res =  Convert.ToInt64(id);
            return res;
        }

        protected async Task<long> Update(T item)
        {
            var id = await DbContext.UpdateAsync(item);
            var res = Convert.ToInt64(id);
            return res;
        }

        public void Dispose()
        {
            if(DbContext != null)
            {
                DbContext.Dispose();
            }
        }
    }
}
