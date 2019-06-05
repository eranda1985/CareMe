using System;

namespace CareMe.IntegrationService
{
	public class FuelRecordAddedEvent: IntegrationEvent
	{
		public long Id { get; set; }

		public long VehicleId { get; set; }

		public string Date { get; set; }

		public double Litres { get; set; }

		public double Price { get; set; }

		public double Mileage { get; set; }
	}
}
