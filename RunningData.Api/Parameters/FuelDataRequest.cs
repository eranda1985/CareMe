using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace RunningData.Api.Parameters
{
    public class FuelDataRequest
    {
        [JsonProperty(PropertyName = "mileage")]
        public string Mileage
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
    }
}
