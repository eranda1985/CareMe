using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Core
{
	public class AppSettings
	{
		public Dictionary<string, string> ConnectionStrings { get; set; }
		public string JWTSecurityKey { get; set; }
	}
}
