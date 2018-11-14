using AutoMapper;
using Microsoft.Extensions.Logging;
using RunningData.Model.Dto;
using RunningData.Model.Models;
using RunningData.Model.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RunningData.Model.Services
{
	public class UserDataService: IService<UserDataDto>
	{
		private readonly IUserDataRepository _userDataRepository;
		private ILogger<UserDataService> _logger;

		public UserDataService(IUserDataRepository userDataRepository,
														ILogger<UserDataService> logger)
		{
			_userDataRepository = userDataRepository;
			_logger = logger;
		}

		public async Task<bool> AddNewUser(UserDataDto userDto)
		{
			var model = Mapper.Map<UserDataModel>(userDto);
			var res = await _userDataRepository.UpsertUser(model);
			return res;
		}
	}
}
