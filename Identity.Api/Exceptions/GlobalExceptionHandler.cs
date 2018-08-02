// 27/07/2018 -- geethamali
using System;
using Identity.Api.ExtensionMethods;
using Identity.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace Identity.Api.Exceptions
{
    public class GlobalExceptionHandler : IExceptionFilter
    {
        void IExceptionFilter.OnException(ExceptionContext context)
        {

            if (context.Exception.IsA<ArgumentNullException>())
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
            else
            {
                context.Result = new StatusCodeResult(500);
            }
        }
    }
}
