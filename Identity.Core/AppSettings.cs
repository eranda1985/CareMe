// 24/07/2018 -- geethamali
using System;
namespace Identity.Core
{
    public class AppSettings
    {
        public string MyKey { get; }
        public int JWTExpiry { get; set; }
        public string JWTSecretKey { get; set; }
    }
}
