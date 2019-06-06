using NPoco;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Model.Models
{
	[TableName("FuelConsumption")]
	[PrimaryKey("Id")]
	public class FuelDetailsModel
	{
		[ResultColumn]
		public long  Id { get; set; }
		[Column("Litres")]
		public double Litres { get; set; }
		[Column("Amount")]
		public double Price { get; set; }
		[Column("Odometer")]
		public double Mileage { get; set; }
		[Column("Date")]
		public DateTime Date { get; set; }
		[Column("VehicleId")]
		public long VehicleId { get; set; }
	}
}
