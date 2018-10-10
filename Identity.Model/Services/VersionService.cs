using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Identity.Core;
using Identity.Core.Validators;
using Identity.Model.Dto;
using Identity.Model.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Identity.Model.Services
{
    public class VersionService : IService<VersionDto>
    {
        private readonly IExceptionService _exceptionService;
        private readonly IConfiguration _configuration;
        private ILogger<VersionService> _logger;
        private AppSettings _appSettings;
        private IVersionRepository _versionRepository;

        public VersionService(IExceptionService exceptionService, IConfiguration configuration, 
                              ILogger<VersionService> logger, IVersionRepository versionRepository)
        {
            _exceptionService = exceptionService;
            _configuration = configuration;
            _logger = logger;
            _versionRepository = versionRepository;
            _appSettings = configuration.Get<AppSettings>();
        }

        /// <summary>
        /// Gets the allowed versions async.
        /// </summary>
        /// <returns>The allowed versions async.</returns>
        /// <param name="args">Arguments.</param>
        public async Task<List<VersionDto>> GetAllowedVersionsAsync(params object[] args)
        {
            _logger.LogDebug("Entering Get Allowed Versions async method");
            _exceptionService.Throw(() => Validator.CheckArgsLength(args, 1));

            var deviceType = args[0] as string;
            _exceptionService.Throw(() => Validator.CheckNull(deviceType));

            var models = await _versionRepository.GetAllowedVersion(deviceType);
            var dtos = Mapper.Map<List<VersionDto>>(models ?? null);

            return dtos;
        }
    }
}
