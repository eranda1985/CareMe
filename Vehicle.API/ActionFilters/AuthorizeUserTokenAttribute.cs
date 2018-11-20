﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Core;

namespace Vehicle.Api.ActionFilters
{
	public class AuthorizeUserTokenAttribute : ActionFilterAttribute
	{
		private IConfiguration _configuration;
		private AppSettings _appSettings;

		public AuthorizeUserTokenAttribute(IConfiguration configuration)
		{

			_configuration = configuration;
			_appSettings = configuration.Get<AppSettings>();
		}

		public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate actionExecutionDelegate)
		{
			var headers = context.HttpContext.Request.Headers;
			var authHeaderValue = headers.Where(x => x.Key == "Authorization").FirstOrDefault().Value;

			if (authHeaderValue.Any())
			{
				var tokenStr = authHeaderValue[0];
				//Remove the Bearer text
				tokenStr = tokenStr.Replace("Bearer ", "");

				JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
				JwtSecurityToken jwtSecToken = tokenHandler.ReadJwtToken(tokenStr);
				var username = jwtSecToken.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault();

				if (username == null)
				{
					context.Result = new UnauthorizedResult();
					return;
				}

				// Get the user record from back-end 
				var userModel = await ((UserDataService)_userService).GetUserByName(username.Value);

				// Using the secret key generate a new token 
				// Compute jwt secret
				var bytes = Encoding.UTF8.GetBytes(string.Concat(_appSettings.JWTSecretKey, userModel.Secret));
				SHA256Managed hash = new SHA256Managed();
				byte[] jwtSecret = hash.ComputeHash(bytes);

				// Construct jwt header
				var secKey = new SymmetricSecurityKey(jwtSecret);
				var signingCredentials = new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256Signature);
				var header = new JwtHeader(signingCredentials);

				// Compare the generated token with the incoming token 
				var generatedToken = new JwtSecurityToken(header, jwtSecToken.Payload);
				var newTokenString = tokenHandler.WriteToken(generatedToken);

				if (tokenStr == newTokenString)
				{
					await actionExecutionDelegate.Invoke();
				}
				else
				{
					context.Result = new UnauthorizedResult();
					return;
				}
			}
			else
			{
				context.Result = new UnauthorizedResult();
			}
		}
	}
}