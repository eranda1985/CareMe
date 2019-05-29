using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analytics.Api.Exceptions
{
	public class GlobalExceptionHandler : IExceptionFilter
	{
		ILogger<GlobalExceptionHandler> _logger;

		public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
		{
			_logger = logger;
		}

		public void OnException(ExceptionContext context)
		{
			_logger.LogError(context.Exception.ToString());

		}
	}
}
