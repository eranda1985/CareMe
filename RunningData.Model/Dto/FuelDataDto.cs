using System;
namespace RunningData.Model.Dto
{
	public class FuelDataDto
	{
		public double Litres { get; set; }
		public double Price { get; set; }
		public double Mileage { get; set; }
		public DateTime Date { get; set; }
		public long VehicleId { get; set; }

	}
}
