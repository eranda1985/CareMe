using System;
using NPoco;

namespace Identity.Model.Models
{
    [TableName("UserDetail")]
    public class UserModel
    {
        [Column("Id")]
        public long Id { get; set; }
        [Column("Username")]
        public string Username { get; set; }
        [Column("Password")]
        public string Password { get; set; }
        [Column("Secret")]
        public string SecretKey { get; set; }
        [Column("DeviceType")]
        public string DeviceType { get; set; }
        [Column("LastLoginDate")]
        public DateTime LastLoginDate { get; set; }
    }
}
