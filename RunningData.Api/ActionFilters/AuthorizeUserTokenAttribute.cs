using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RunningData.Model.Dto;
using RunningData.Model.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RunningData.Api.ActionFilters
{
    public class AuthorizeUserTokenAttribute: ActionFilterAttribute
    {
        private IService<FuelDataDto> _service;

        public AuthorizeUserTokenAttribute(IService<FuelDataDto> service)
        {
            _service = service;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var headers = context.HttpContext.Request.Headers;
            var authHeaderValue = headers.Where(x => x.Key == "Authorization").FirstOrDefault().Value;

            if (authHeaderValue.Any())
            {
                var tokenStr = authHeaderValue[0];
                //Remove the Bearer text
                tokenStr = tokenStr.Replace("Bearer ", "");

                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken  jwtSecToken = tokenHandler.ReadJwtToken(tokenStr);
                var username = jwtSecToken.Claims.Where(x=>x.Type == ClaimTypes.NameIdentifier).FirstOrDefault();

                // Get the user record from back-end 
                // Using the secret key generate a new token 
                // Compare the generated token with the incoming token 
            }
            else
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
