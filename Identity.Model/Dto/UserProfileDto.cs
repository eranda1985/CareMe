using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Model.Dto
{
    public class UserProfileDto
    {
        public string Username { get; set; }
        public string Fullname => string.Format("{0} {1}", First, Last);
        public string First { get; set; }
        public string Last { get; set; }
        public string Mobile { get; set; }
        public long Id { get; set; }
    }
}
