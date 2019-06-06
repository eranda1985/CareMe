using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using NPoco;

namespace RunningData.Model.Repositories
{
    public class DBRepository<T> where T : class
    {
        protected Database InternalContext { get; set; }

        /// <summary>
        /// Query the specified query and args.
        /// </summary>
        /// <returns>The query.</returns>
        /// <param name="query">Query.</param>
        /// <param name="args">Arguments.</param>
        protected async Task<List<T>> Query(string query, params object[] args)
        {
            Sql q = new Sql(query, args);
            var result = await InternalContext.FetchAsync<T>(q);
            return result;
        }

        /// <summary>
        /// Add the specified item.
        /// </summary>
        /// <returns>The add.</returns>
        /// <param name="item">Item.</param>
        protected async Task<long> Add(T item)
        {
            var id = await InternalContext.InsertAsync<T>(item);
            var res =  Convert.ToInt64(id);
            return res;
        }

        protected async Task<long> Update(T item)
        {
            var id = await InternalContext.UpdateAsync(item);
            var res = Convert.ToInt64(id);
            return res;
        }
    }
}
