using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Model.Dto;
using Vehicle.Model.Models;
using Vehicle.Model.Repositories.Interfaces;

namespace Vehicle.Model.Services
{
    public class UserdataService: IService<UserDataDto>
    {
        IExceptionService _exceptionService;
        IUserDataRepository _userDataRepository;

        public UserdataService(IExceptionService exceptionService,
            IUserDataRepository userDataRepository)
        {
            _exceptionService = exceptionService;
            _userDataRepository = userDataRepository;
        }

        public async Task<bool> CreateNewUser(UserDataDto dto)
        {
            var model = Mapper.Map<UserDataModel>(dto);
            return await _userDataRepository.UpsertUser(model);
        }

        public async Task<UserDataDto> GetUserByName(string name)
        {
            var res = await _userDataRepository.GetUserByName(name);
            return new UserDataDto { Username = res.UserName, Secret = res.SecretKey };
        }
    }
}
