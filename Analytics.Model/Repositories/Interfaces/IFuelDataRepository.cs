﻿using Analytics.Model.Models;
using NPoco;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Model.Repositories.Interfaces
{
	public interface IFuelDataRepository<U>: IDisposable where U : FuelDetailsModel
	{
		void SetDBContext(Database context);
		Task<long> AddNew(U poco);
		Task<bool> DeleteEntry(U poco);
		Task<List<U>> GetRecentFuelEntries();
		Task<List<U>> GetBackwardEntriesFromOffset(DateTime seed);
		Task<List<U>> GetForewardEntriesFromOffset(DateTime seed);
		Task<U> GetEntryById(long id);
	}
}