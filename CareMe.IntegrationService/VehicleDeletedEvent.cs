using System;
using System.Collections.Generic;
using System.Text;

namespace CareMe.IntegrationService
{
	public class VehicleDeletedEvent: IntegrationEvent
	{
		public long VehicleId { get; set; }
	}
}
