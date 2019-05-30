using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicle.Model.Dto
{
	public class VehicleModelDto
	{
		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("brandId")]
		public long BrandId { get; set; }

		[JsonProperty("model")]
		public string Model { get; set; }
	}
}
