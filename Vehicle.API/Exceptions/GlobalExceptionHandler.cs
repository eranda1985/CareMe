// 27/07/2018 -- geethamali
using System;
using System.Data.SqlTypes;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Vehicle.Api.ExtensionMethods;
using Vehicle.Core.Exceptions;

namespace Vehicle.Api.Exceptions
{
    public class GlobalExceptionHandler : IExceptionFilter
    {
        ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }
        void IExceptionFilter.OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception.ToString());

            if (context.Exception.IsA<ArgumentNullException>())
            {
                context.Result = new BadRequestResult();
			}

            else if (context.Exception.IsA<UnauthorizedAccessException>())
            {
                context.Result = new UnauthorizedResult();
								context.HttpContext.Response.Headers.Add("Error", context.Exception.Message);
			}

            else if (context.Exception.IsA<ValidationException>())
            {
                context.Result = new BadRequestResult();
								context.HttpContext.Response.Headers.Add("Error", context.Exception.Message);
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
