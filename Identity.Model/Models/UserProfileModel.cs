using NPoco;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Model.Models
{
    [TableName("UserProfile")]
    [PrimaryKey("Id", AutoIncrement = false)]
    public class UserProfileModel
    {
        [ResultColumn]
        public long Id { get; set; }

        [Column("Username")]
        public string Username { get; set; }

        [Column("FirstName")]
        public string FirstName { get; set; }

        [Column("LastName")]
        public string LastName { get; set; }

        [Column("MobilePhoneNumber")]
        public string  Mobile { get; set; }
    }
}
