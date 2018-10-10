using System;
using Newtonsoft.Json;

namespace Identity.Model.Dto
{
    public class AuthenticationResponseDto
    {
        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }

        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        [JsonProperty(PropertyName = "signUpCode")]
        public string SignUpCode { get; set; }
    }
}
