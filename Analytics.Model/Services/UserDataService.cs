using Analytics.Core.Validators;
using Analytics.Model.Dto;
using Analytics.Model.Models;
using Analytics.Model.Repositories;
using Analytics.Model.Repositories.Interfaces;
using AutoMapper;
using System.Threading.Tasks;

namespace Analytics.Model.Services
{
	public class UserDataService : IService<UserDataDto>
	{
		private readonly IExceptionService _exceptionService;
		private readonly IUserDataRepository<UserDataModel> _userRepository;

		public UserDataService(
			IExceptionService exceptionService,
			IUserDataRepository<UserDataModel> userDataRepository)
		{
			_exceptionService = exceptionService;
			_userRepository = userDataRepository;
		}

		/// <summary>
		/// Creates a user. Use this as an atomic method. 
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public async Task<bool> CreateUser(UserDataDto user)
		{
			_exceptionService.Throw(() => Validator.CheckNull(user));
			_exceptionService.Throw(() => Validator.CheckNull(((UserDataRepository)_userRepository).DBContext));

			var model = Mapper.Map<UserDataModel>(user);
			bool result = false;

			// Be cautious when using this block inside another using block. It may lead to Db context being disposed before the complete operation. 
			using (_userRepository)
			{
				result = await _userRepository.Upsert(model);
			}

			return result;
		}

		/// <summary>
		/// Get user by name. Use this as an atomic method
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public async Task<UserDataDto> GetUserByName(string name)
		{
			UserDataModel modelObject;
			_exceptionService.Throw(() => Validator.CheckNull(((UserDataRepository)_userRepository).DBContext));

			// Be cautious when using this block inside another using block. It may lead to Db context being disposed before the complete operation. 
			using (_userRepository)
			{
				modelObject = await _userRepository.GetUserByName(name);
			}

			_exceptionService.Throw(() => Validator.CheckNull(modelObject));

			return new UserDataDto { Username = modelObject.UserName, Secret = modelObject.SecretKey, Id = modelObject.Id };
		}
	}
}
