using System;
using System.Threading.Tasks;
using Identity.Core.Validators;
using Identity.Model.Dto;
using Identity.Model.Models;
using Identity.Model.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Identity.Model.Services
{
    public class UserService: IService<UserDto>
    {
        private readonly IExceptionService _exceptionService;
        private readonly IUserRepository _userRepository;
        private ILogger<UserService> _logger;

        public UserService(ILogger<UserService> logger, IExceptionService exceptionService,
                           IUserRepository userRepo)
        {
            _exceptionService = exceptionService;
            _userRepository = userRepo;
            _logger = logger;
        }

        /// <summary>
        /// Logins the async.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="args">Arguments.</param>
        public async Task<AuthenticationResponseDto> LoginAsync(params object[] args)
        {
            _logger.LogDebug("Entering LoginAsync");

            var userName = args[0] as string;
            var password = args[1] as string;

            _exceptionService.Throw(() => { Validator.CheckNull(userName); });

            // Get the user record based on username
            var user = await _userRepository.GetUserByNameAsync(userName);

            // Check for app versions. 

            // Ensure the passwords match. 
            if (user?.Password != password)
            {
                _exceptionService.Throw(() => Validator.UnAuthorized());
            }

            // Get the token from Db and return 
            return new AuthenticationResponseDto { Token = "1234", Username = user.Username };

        }

        /// <summary>
        /// Adds the user async.
        /// </summary>
        /// <returns>The user async.</returns>
        /// <param name="args">Arguments.</param>
        public async Task<AuthenticationResponseDto> RegisterUserAsync(params object[] args)
        {
            var username = args[0] as string;
            var pwd = args[1] as string;

            _exceptionService.Throw(() => { Validator.CheckStringEmpty(username); });
            _exceptionService.Throw(() => { Validator.CheckStringEmpty(pwd); });

            var newUser = await _userRepository.AddUserAsync(new UserModel { Username = username, Password = pwd });

            return new AuthenticationResponseDto { Token = "1234", Username = newUser.Username };
        }
    }
}
