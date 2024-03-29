﻿using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace RunningData.Api.Parameters
{
	public class FuelDataRequest
	{
		[JsonProperty(PropertyName = "mileage")]
		public double Mileage
		{
			get;
			set;
		}

		[Required]
		[JsonProperty(PropertyName = "date")]
		public string Date
		{
			get;
			set;
		}

		[DataType(DataType.Currency)]
		[JsonProperty(PropertyName = "price")]
		public double Price
		{
			get;
			set;
		}

		[JsonProperty(PropertyName = "litres")]
		public double Litres
		{
			get;
			set;
		}

		[JsonProperty("vehicleid")]
		public long VehicleId { get; set; }
	}
}
