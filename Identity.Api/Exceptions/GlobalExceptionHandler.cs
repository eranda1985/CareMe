// 27/07/2018 -- geethamali
using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Identity.Api.Exceptions
{
    public class GlobalExceptionHandler : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ArgumentNullException)
            {
                context.Result = new BadRequestResult();
            }

            if (context.Exception is UnauthorizedAccessException)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
