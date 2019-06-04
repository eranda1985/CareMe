using System;
using System.Collections.Generic;
using System.Text;

namespace CareMe.IntegrationService
{
	public class NewVehicleAddedEvent: IntegrationEvent
	{
		public long VehicleId { get; set; }
		public string Rego { get; set; }
		public double LastODOMeter { get; set; }
		public DateTime LastUpdated { get; set; }
	}
}
