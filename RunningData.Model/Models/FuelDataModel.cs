using System;
using NPoco;

namespace RunningData.Model.Models
{
	[TableName("FuelData")]
	[PrimaryKey("Id")]
	public class FuelDataModel
	{
		[ResultColumn]
		public long Id { get; set; }

		[Column("VehicleId")]
		public long VehicleId { get; set; }

		[Column("Date")]
		public DateTime Date { get; set; }

		[Column("Litres")]
		public double Litres { get; set; }

		[Column("Price")]
		public double Price { get; set; }

		[Column("Mileage")]
		public double Mileage { get; set; }
	}
}
