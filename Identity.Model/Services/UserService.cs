using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CareMe.IntegrationService;
using Identity.Core;
using Identity.Core.Security;
using Identity.Core.Security.KeyGenerators;
using Identity.Core.Validators;
using Identity.Model.Dto;
using Identity.Model.Models;
using Identity.Model.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Identity.Model.Services
{
    public class UserService : IService<UserDto>
    {
        private readonly IExceptionService _exceptionService;
        private readonly IUserRepository _userRepository;
        private IConfiguration _configuration;
        private readonly AppSettings _appSettings;
        private readonly IService<EmailDto> _emailService;
        private readonly IServiceBus _serviceBus;

        private ILogger<UserService> _logger;

        public UserService(ILogger<UserService> logger, IExceptionService exceptionService,
                           IUserRepository userRepo,
                           IConfiguration configuration,
                           IService<EmailDto> emailService,
                           IServiceBus serviceBus)
        {
            _exceptionService = exceptionService;
            _userRepository = userRepo;
            _logger = logger;
            _configuration = configuration;
            _appSettings = configuration.Get<AppSettings>();
            _emailService = emailService;
            _serviceBus = serviceBus;
        }

        /// <summary>
        /// Logins the async.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="args">Arguments.</param>
        public async Task<AuthenticationResponseDto> LoginAsync(params object[] args)
        {
            _logger.LogDebug("Entering LoginAsync");
            _exceptionService.Throw(() => Validator.CheckArgsLength(args, 4));

            var userName = args[0] as string;
            var password = args[1] as string;
            var versionHash = args[2] as string;
            var deviceType = args[3] as string;

            _exceptionService.Throw(() => { Validator.CheckNull(userName); });

            // Get the user record based on username
            var user = await _userRepository.GetUserByNameAsync(userName);
            var userDto = AutoMapper.Mapper.Map<UserDto>(user ?? null);

            if (user == null)
            {
                var emailService = _emailService as EmailService;
                emailService.SendMail("eranda.lakshantha@gmail.com", "eranda1985@yahoo.com");
                return new AuthenticationResponseDto { Token = "", Username = userName, Password = password, SignUpCode = "0000" };

            }
            // TODO: Check for app versions. //

            // Ensure the passwords match. 
            if (userDto?.Password != password)
            {
                _exceptionService.Throw(Validator.UnAuthorized);
            }

            // Update last login date
            user.LastLoginDate = DateTime.Now;
            await _userRepository.UpdateUserAsync(user);

            // Create claims 
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userDto.Username),
                new Claim(ClaimTypes.Version, versionHash),
                new Claim(ClaimTypes.System, userDto.DeviceType)
            };

            var token = JWTHelper.GenerateUserToken(claims, userDto.SecretKey, _appSettings.JWTSecretKey, _appSettings.JWTExpiry);

            // Get the token from Db and return 
            return new AuthenticationResponseDto { Token = token, Username = userDto.Username };

        }

        /// <summary>
        /// Adds the user async.
        /// </summary>
        /// <returns>The user async.</returns>
        /// <param name="args">Arguments.</param>
        public async Task<UserDto> RegisterUserAsync(params object[] args)
        {
            _exceptionService.Throw(() => Validator.CheckArgsLength(args, 4));

            var username = args[0] as string;
            var pwd = args[1] as string;
            var versionHash = args[2] as string;
            var deviceType = args[3] as string;

            _exceptionService.Throw(() => { Validator.CheckStringEmpty(username); });
            _exceptionService.Throw(() => { Validator.CheckStringEmpty(pwd); });
            _exceptionService.Throw(() => { Validator.CheckStringEmpty(versionHash); });

            var id = await _userRepository.AddUserAsync(new UserModel
            {
                Username = username,
                Password = pwd,
                SecretKey = SecretKey.GenerateKey(64),
                DeviceType = deviceType,
                LastLoginDate = DateTime.Now
            });

            var newUser = await _userRepository.GetUserByIdAsync(id);
            var dto = AutoMapper.Mapper.Map<UserDto>(newUser);
            return dto;
        }

        /// <summary>
        /// Sign up the user async
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public async Task<AuthenticationResponseDto> SignUpUserAsync(params object[] args)
        {
            var userName = args[0] as string;
            var password = args[1] as string;
            var versionHash = args[2] as string;
            var deviceType = args[3] as string;

            _exceptionService.Throw(() => { Validator.CheckNull(userName); });

            // Get the user record based on username
            var user = await _userRepository.GetUserByNameAsync(userName);

            if (user != null)
            {
                _exceptionService.Throw(() => Validator.UnAuthorized());
            }

            var userDto = await RegisterUserAsync(userName, password, versionHash, deviceType);
            user = new UserModel
            {
                Id = userDto.Id,
                DeviceType = userDto.DeviceType,
                LastLoginDate = userDto.LastLoginDate,
                Password = userDto.Password,
                SecretKey = userDto.SecretKey,
                Username = userDto.Username
            };

            // Ensure the passwords match.
            if (userDto?.Password != password)
            {
                _exceptionService.Throw(Validator.UnAuthorized);
            }

            // Make sure to communicate this new user to RunningData API for integration purposes. 
            var msg = new IdentityUserAddedEvent(userDto.Username, userDto.SecretKey);
            _logger.LogDebug("Publish Message username: {0} secret: {1}", msg.Username, msg.UserSecret);
            _serviceBus.Publish<IdentityUserAddedEvent>(msg);

            // Update last login date
            user.LastLoginDate = DateTime.Now;
            await _userRepository.UpdateUserAsync(user);

            // Create claims 
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userDto.Username),
                new Claim(ClaimTypes.Version, versionHash),
                new Claim(ClaimTypes.System, userDto.DeviceType)
            };

            var token = JWTHelper.GenerateUserToken(claims, userDto.SecretKey, _appSettings.JWTSecretKey, _appSettings.JWTExpiry);

            // Get the token from Db and return 
            return new AuthenticationResponseDto { Token = token, Username = userDto.Username };

        }
    }
}
