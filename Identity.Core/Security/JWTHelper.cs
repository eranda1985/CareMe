using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Identity.Core.Security
{
    public class JWTHelper
    {
        /// <summary>
        /// Generates the user token.
        /// </summary>
        /// <param name="claims">Claims.</param>
        /// <param name="secretKey">Secret key.</param>
        public static string GenerateUserToken(List<Claim> claims, string secretKey, string JWTSecretKey, int expiryDays)
        {
            // Compute jwt secret
            var bytes = Encoding.UTF8.GetBytes(string.Concat(JWTSecretKey, secretKey));
            SHA256Managed hash = new SHA256Managed();
            byte[] jwtSecret = hash.ComputeHash(bytes);

            // Construct jwt header
            var secKey = new SymmetricSecurityKey(jwtSecret);
            var signingCredentials = new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256Signature);
            var header = new JwtHeader(signingCredentials);

            // Add token expiry
            var expiry = DateTime.UtcNow.AddDays(expiryDays);
            claims.Add(new Claim(ClaimTypes.Expiration, expiry.ToString("yyyy-MM-ddTHH:mm:ssZ")));

            // Construct the payload
            var payload = new JwtPayload(claims);

            // Generate token string
            var token = new JwtSecurityToken(header, payload);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
    }
}
