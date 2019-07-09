using Identity.Api.Parameters;
using Identity.Model.Dto;
using Identity.Model.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Identity.Api.Controllers
{
	[Route("authentication")]
	[Produces("application/json")]
	[ApiVersion("1.0")]
	public class LoginController : Controller
	{
		private readonly ILogger<LoginController> _logger;
		private readonly IConfiguration _configuration;
		private readonly IService<UserDto> _userService;
		private readonly IService<VersionDto> _versionService;
		private string _assemblyDirectory
		{
			get
			{
				string codeBase = Assembly.GetExecutingAssembly().CodeBase;
				UriBuilder uri = new UriBuilder(codeBase);
				string path = Uri.UnescapeDataString(uri.Path);
				return Path.GetDirectoryName(path);
			}
		}

		public LoginController(ILogger<LoginController> logger,
															 IConfiguration configuration,
															 IService<UserDto> userService,
															 IService<VersionDto> versionservice)
		{
			_logger = logger;
			_configuration = configuration;
			_userService = userService;
			_versionService = versionservice;
		}

		[HttpPost]
		[Route("login")]
		[ProducesResponseType(200, Type = typeof(AuthenticationResponseDto))]
		[ProducesResponseType(401)]
		[MapToApiVersion("1.0")]
		public async Task<IActionResult> Login([FromBody]AuthRequest requestBody)
		{
			_logger.LogDebug(string.Format("Entering Login controller v1.0 {0}.", _configuration["MyKey"]));

			// Get the token from Db and return 
			var res = await ((UserService)_userService).LoginAsync(requestBody.Username, requestBody.Password, requestBody.VersionHash, requestBody.DeviceType, _assemblyDirectory);

			return Ok(res);
		}

		[HttpPost]
		[Route("signup")]
		[ProducesResponseType(200, Type = typeof(AuthenticationResponseDto))]
		[ProducesResponseType(401)]
		[MapToApiVersion("1.0")]
		public async Task<IActionResult> SignUp([FromBody]AuthRequest requestBody)
		{
			_logger.LogDebug("Entering sign-up controller");

			var auth = await ((UserService)_userService).SignUpUserAsync(requestBody.Username, requestBody.Password, requestBody.VersionHash, requestBody.DeviceType);

			return Ok(auth);
		}

		[HttpGet]
		[Route("version/{devicetype}")]
		[ProducesResponseType(200, Type = typeof(List<VersionDto>))]
		[ProducesResponseType(401)]
		[MapToApiVersion("1.0")]
		public async Task<IActionResult> GetVersions([FromRoute] string devicetype)
		{
			_logger.LogDebug("Entering version check");

			var res = await ((VersionService)_versionService).GetAllowedVersionsAsync(devicetype);

			return Ok(res);
		}
	}
}
