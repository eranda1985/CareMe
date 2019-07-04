using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vehicle.API.Parameters
{
	public class VehicleDataUpdateRequest
	{
		[JsonProperty(PropertyName = "id")]
		public long Id { get; set; }

		[JsonProperty(PropertyName = "type")]
		public string VehicleType { get; set; }

		[JsonProperty(PropertyName = "brand")]
		public string Brand { get; set; }

		[JsonProperty(PropertyName = "model")]
		public string Model { get; set; }

		[JsonProperty(PropertyName = "fuel")]
		public string FuelType { get; set; }

		[JsonProperty(PropertyName = "rego")]
		[Required]
		public string RegoPlate { get; set; }

		[JsonProperty(PropertyName = "date")]
		public string Date { get; set; }

		[JsonProperty(PropertyName = "odo")]
		public double ODOMeter { get; set; }

		[JsonProperty(PropertyName = "username")]
		public string Username { get; set; }
	}
}
