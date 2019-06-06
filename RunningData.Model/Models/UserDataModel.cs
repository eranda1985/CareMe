using NPoco;

namespace RunningData.Model.Models
{
	[TableName("UserData")]
	[PrimaryKey("Id", AutoIncrement = false)]
	public class UserDataModel
	{
		[Column("UserName")]
		public string UserName { get; set; }
		[Column("SecretKey")]
		public string SecretKey { get; set; }
		[ResultColumn("Id")]
		public long Id { get; set; }
	}
}
