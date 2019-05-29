using NPoco;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicle.Model.Models
{
	[TableName("VehicleUsers")]
	public class VehicleUserDataModel
	{
        [ResultColumn("Id")]
        public long Id { get; set; }
        [Column("VehicleId")]
		public long VehicleId { get; set; }
		[Column("Username")]
		public string Username { get; set; }
		[Column("IsDefault")]
		public bool IsDefault { get; set; }
	}
}
