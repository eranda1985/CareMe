using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using NPoco;

namespace Identity.Model.Repositories
{
    public class DBRepository<T> where T : class
    {
        private Database _dbContext = null;

        public DBRepository(string connectionString, DatabaseType databaseType, DbProviderFactory dbProvideerFactory)
        {
            _dbContext = new Database(connectionString, databaseType, dbProvideerFactory);
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
            var result = await _dbContext.FetchAsync<T>(q);
            return result;
        }

        /// <summary>
        /// Add the specified item.
        /// </summary>
        /// <returns>The add.</returns>
        /// <param name="item">Item.</param>
        protected async Task<T> Add(T item)
        {
            var obj = await _dbContext.InsertAsync<T>(item);
            return obj as T;
        }
    }
}
