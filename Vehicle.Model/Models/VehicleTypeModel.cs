using NPoco;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicle.Model.Models
{
	[TableName("VehicleTypes")]
	public class VehicleTypeModel
	{
		[ResultColumn("Id")]
		public long Id { get; set; }

		[Column("Type")]
		public string Type { get; set; }
	}
}
