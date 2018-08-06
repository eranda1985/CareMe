using System;
namespace Identity.Model.Dto
{
    public class AuthenticationResponseDto
    {
        public string Token { get; set; }
        public string Username { get; set; }
    public string Password { get; set; }
    public string SignUpCode { get; set; }
    }
}
