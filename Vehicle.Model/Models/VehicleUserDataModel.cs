using NPoco;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicle.Model.Models
{
	[TableName("VehicleData")]
	public class VehicleUserDataModel
	{
		[Column("VehicleId")]
		public long VehicleId { get; set; }
		[Column("UserId")]
		public long UserId { get; set; }
		[Column("IsDefault")]
		public bool IsDefault { get; set; }
	}
}
