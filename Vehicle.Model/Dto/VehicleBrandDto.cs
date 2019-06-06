using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicle.Model.Dto
{
	public class VehicleBrandDto
	{
		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("brand")]
		public string Brand { get; set; }
	}
}
