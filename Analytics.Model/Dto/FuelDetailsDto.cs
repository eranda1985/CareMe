using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Model.Dto
{
	public class FuelDetailsDto
	{
		public double Litres { get; set; }
		public double Price { get; set; }
		public double Mileage { get; set; }
		public DateTime Date { get; set; }
		public long VehicleId { get; set; }
	}
}
