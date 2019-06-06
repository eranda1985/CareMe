using NPoco;
using System;

namespace Analytics.Model.Models
{
	[TableName("VehicleDetails")]
	[PrimaryKey("VehicleId", AutoIncrement = false)]
	public class VehicleDetailsModel
	{
		[ResultColumn]
		public long Id { get; set; }

		[Column("VehicleId")]
		public long VehicleId { get; set; }

		[Column("Rego")]
		public string Rego { get; set; }

		[Column("LastOdometer")]
		public double LastODOMeter { get; set; }

		[Column("LastUpdated")]
		public DateTime LastUpdated { get; set; }
	}
}
