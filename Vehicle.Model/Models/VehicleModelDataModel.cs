using NPoco;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicle.Model.Models
{
	[TableName("VehicleModels")]
	public class VehicleModelDataModel
	{
		[ResultColumn("Id")]
		public long Id { get; set; }

		[Column("BrandId")]
		public long BrandId { get; set; }

		[Column("Model")]
		public string Model { get; set; }
	}
}
