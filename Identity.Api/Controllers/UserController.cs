using Identity.Api.Parameters;
using Identity.Model.Dto;
using Identity.Model.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.Api.Controllers
{
	[Route("user")]
	[Produces("application/json")]
	[ApiVersion("1.0")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly ILogger<LoginController> _logger;
		private readonly IService<UserDto> _userService;

		public UserController(
			ILogger<LoginController> logger,
			IService<UserDto> userService)
		{
			_logger = logger;
			_userService = userService;
		}

		[HttpGet]
		[Route("profile/{username}")]
		[ProducesResponseType(200, Type = typeof(List<UserProfileDto>))]
		[ProducesResponseType(401)]
		[MapToApiVersion("1.0")]
		public async Task<IActionResult> GetProfile([FromRoute] string username)
		{
			_logger.LogDebug("Entering user profile resource");

			var res = await ((UserService)_userService).GetUserProfile(username);

			return Ok(res);
		}

		[HttpPost]
		[Route("profile")]
		[ProducesResponseType(200, Type = typeof(bool))]
		[ProducesResponseType(401)]
		[MapToApiVersion("1.0")]
		public async Task<IActionResult> AfddUserProfile([FromBody] UserProfileRequest requestBody)
		{
			_logger.LogDebug("Entering add user profile endpoint");

			var res = await ((UserService)_userService)
				.AddUserProfile(requestBody.Username, requestBody.First, requestBody.Last, requestBody.Mobile);

			return Ok(res);
		}
	}
}