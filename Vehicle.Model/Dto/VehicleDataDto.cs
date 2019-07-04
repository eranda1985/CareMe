using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicle.Model.Dto
{
    public class VehicleDataDto
    {

		[JsonProperty(PropertyName = "id")]
		public long Id { get; set; }

		[JsonProperty(PropertyName = "vehicletype")]
		public string VehicleType { get; set; }

		[JsonProperty(PropertyName = "brand")]
		public string Brand { get; set; }

		[JsonProperty(PropertyName = "model")]
		public string Model { get; set; }

		[JsonProperty(PropertyName = "fueltype")]
		public string FuelType { get; set; }

		[JsonProperty(PropertyName = "rego")]
		public string RegoPlate { get; set; }

		[JsonProperty(PropertyName = "date")]
		public DateTime Date { get; set; }

		[JsonProperty(PropertyName = "odometer")]
		public double ODOMeter { get; set; }

		[JsonProperty(PropertyName = "isdefault")]
		public bool IsSelected { get; set; }
	}
}
