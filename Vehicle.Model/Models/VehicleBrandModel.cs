using NPoco;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicle.Model.Models
{
	[TableName("VehicleBrands")]
	public class VehicleBrandModel
	{
		[ResultColumn("Id")]
		public long Id { get; set; }

		[Column("Brand")]
		public string Brand { get; set; }
	}
}
