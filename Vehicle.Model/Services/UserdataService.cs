using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Vehicle.Model.Dto;
using Vehicle.Model.Models;
using Vehicle.Model.Repositories.Interfaces;

namespace Vehicle.Model.Services
{
	public class UserdataService : IService<UserDataDto>
	{
		private readonly IExceptionService _exceptionService;
		private readonly IUserDataRepository _userDataRepository;
		private readonly ILogger<UserdataService> _logger;

		public UserdataService(
			IExceptionService exceptionService,
				IUserDataRepository userDataRepository,
				ILogger<UserdataService> logger)
		{
			_exceptionService = exceptionService;
			_userDataRepository = userDataRepository;
			_logger = logger;
		}

		public async Task<bool> CreateNewUser(UserDataDto dto)
		{
			var model = Mapper.Map<UserDataModel>(dto);
			return await _userDataRepository.UpsertUser(model);
		}

		public async Task<UserDataDto> GetUserByName(string name)
		{
			_logger.LogDebug("Vehicle API - Get userby name: {0}", name);
			var res = await _userDataRepository.GetUserByName(name);
			return new UserDataDto { Username = res.UserName, Secret = res.SecretKey, Id = res.Id };
		}
	}
}
