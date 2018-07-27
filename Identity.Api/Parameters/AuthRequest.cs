using System;
using System.ComponentModel.DataAnnotations;

namespace Identity.Api.Parameters
{
    public class AuthRequest
    {
        [Required]
        [EmailAddress]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
