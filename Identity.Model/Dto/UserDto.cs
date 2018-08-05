using System;
namespace Identity.Model.Dto
{
    public class UserDto
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string DeviceType { get; set; }
        public string SecretKey { get; set; }
        public DateTime LastLoginDate { get; set; }
    }
}
