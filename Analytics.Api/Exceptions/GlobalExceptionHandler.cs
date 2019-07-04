using Analytics.Api.Extensions;
using Analytics.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Data.SqlTypes;

namespace Analytics.Api.Exceptions
{
	public class GlobalExceptionHandler : IExceptionFilter
	{
		private readonly ILogger<GlobalExceptionHandler> _logger;

		public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
		{
			_logger = logger;
		}

		public void OnException(ExceptionContext context)
		{
			_logger.LogError(context.Exception.ToString());
			context.HttpContext.Response.Headers.Add("Error", context.Exception.Message);

			if (context.Exception.IsA<ArgumentException>())
			{
				context.Result = new BadRequestResult();
			}

			else if (context.Exception.IsA<UnauthorizedAccessException>())
			{
				context.Result = new UnauthorizedResult();
			}

			else if (context.Exception.IsA<ValidationException>())
			{
				context.Result = new BadRequestResult();
			}
			else if (context.Exception.IsA<SqlTypeException>())
			{
				context.Result = new BadRequestResult();
			}
			else
			{
				context.Result = new StatusCodeResult(500);
			}

		}
	}
}
