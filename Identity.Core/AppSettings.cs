// 24/07/2018 -- geethamali
using System;
using System.Collections.Generic;

namespace Identity.Core
{
  public class AppSettings
  {
    public string MyKey { get; }
    public int JWTExpiry { get; set; }
    public string JWTSecretKey { get; set; }
    public Dictionary<string, string> ConnectionStrings { get; set; }

    public string SmtpUserName { get; set; }
    public string SmtpPassword { get; set; }
    public string SmtpProvider { get; set; }
    public int SmtpPort { get; set; }
		public string EmailFrom { get; set; }
	}
}
