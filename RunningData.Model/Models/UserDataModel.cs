using NPoco;
using System;
using System.Collections.Generic;
using System.Text;

namespace RunningData.Model.Models
{
	[TableName("UserData")]
	public class UserDataModel
	{
		[Column("UserName")]
		public string UserName { get; set; }
		[Column("SecretKey")]
		public string SecretKey { get; set; }
	}
}
