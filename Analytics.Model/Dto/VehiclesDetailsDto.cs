using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Model.Dto
{
	public class VehiclesDetailsDto
	{
		public long Id { get; set; }
		public long VehicleId { get; set; }
		public string Rego { get; set; }
		public double LastODOMeter { get; set; }
		public DateTime LastUpdated { get; set; }
	}
}
