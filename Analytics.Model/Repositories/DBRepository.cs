using NPoco;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace Analytics.Model.Repositories
{
	public class DBRepository<T> where T : class
	{
		protected Database InternalContext{ get; set; }

		protected async Task<List<T>> Query(string query, params object[] args)
		{
			Sql q = new Sql(query, args);
			var result = await InternalContext.FetchAsync<T>(q);
			return result;
		}

		
		protected async Task<long> Add(T item)
		{
			var id = await InternalContext.InsertAsync<T>(item);
			var res = Convert.ToInt64(id);
			return res;
		}

		protected async Task<long> Update(T item)
		{
			var id = await InternalContext.UpdateAsync(item);
			var res = Convert.ToInt64(id);
			return res;
		}

		protected async Task<long> Delete(T item)
		{
			var id = await InternalContext.DeleteAsync(item);
			var res = Convert.ToInt64(id);
			return res;
		}
	}
}
