﻿using RunningData.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RunningData.Model.Repositories.Interfaces
{
	public interface IUserDataRepository: IRepository<UserDataModel>
	{
		Task<bool> UpsertUser(UserDataModel user);
        Task<UserDataModel> GetUserByName(string username);
	}
}