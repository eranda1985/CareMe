﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
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
    public class UserService: IService<UserDto>
    {
        private readonly IExceptionService _exceptionService;
        private readonly IUserRepository _userRepository;
        private IConfiguration _configuration;
        private readonly AppSettings _appSettings;

        private ILogger<UserService> _logger;

        public UserService(ILogger<UserService> logger, IExceptionService exceptionService,
                           IUserRepository userRepo,
                           IConfiguration configuration)
        {
            _exceptionService = exceptionService;
            _userRepository = userRepo;
            _logger = logger;
            _configuration = configuration;
            _appSettings = configuration.Get<AppSettings>();
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
            var versionHash = args[2] as string;

            _exceptionService.Throw(() => { Validator.CheckNull(userName); });

            // Get the user record based on username
            var user = await _userRepository.GetUserByNameAsync(userName);

            if(user == null)
            {
                return await RegisterUserAsync(userName, password, versionHash);
            }
            // TODO: Check for app versions. //

            // Ensure the passwords match. 
            if (user?.Password != password)
            {
                _exceptionService.Throw(Validator.UnAuthorized);
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
            _exceptionService.Throw(() => Validator.CheckArgsLength(args, 4));

            var username = args[0] as string;
            var pwd = args[1] as string;
            var versionHash = args[2] as string;

            _exceptionService.Throw(() => { Validator.CheckStringEmpty(username); });
            _exceptionService.Throw(() => { Validator.CheckStringEmpty(pwd); });
            _exceptionService.Throw(() => { Validator.CheckStringEmpty(versionHash); });

            // Create claims 
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, username),
                new Claim(ClaimTypes.Version, versionHash)
            };

            var newUser = await _userRepository.AddUserAsync(new UserModel
            {
                Username = username,
                Password = pwd,
                SecretKey = SecretKey.GenerateKey(64)
            });

            // TODO: Persist the new user in the underlying Data model.

            var token = JWTHelper.GenerateUserToken(claims, newUser.SecretKey, _appSettings.JWTSecretKey, _appSettings.JWTExpiry);

            return new AuthenticationResponseDto { Token = token, Username = newUser.Username };
        }
    }
}
