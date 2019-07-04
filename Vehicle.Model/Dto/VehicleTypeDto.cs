using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicle.Model.Dto
{
	public class VehicleTypeDto
	{
		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("type")]
		public string Type { get; set; }
	}
}
