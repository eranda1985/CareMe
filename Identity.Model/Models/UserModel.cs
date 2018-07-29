using System;
namespace Identity.Model.Models
{
    public class UserModel
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string SecretKey { get; set; }
    }
}
