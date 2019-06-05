using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Analytics.Api.Controllers
{
	[Route("analytics")]
	[ApiController]
	[ApiVersion("1.0")]
	public class PingController : ControllerBase
	{
		[HttpGet]
		[Route("ping")]
		[ProducesResponseType(200)]
		[MapToApiVersion("1.0")]
		public async Task<IActionResult> Ping()
		{
			return Ok("Reply from Analytics API.");
		}
	}
}