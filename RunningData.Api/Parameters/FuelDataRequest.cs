using System;
using System.ComponentModel.DataAnnotations;

namespace RunningData.Api.Parameters
{
    public class FuelDataRequest
    {
        public string Mileage
        {
            get;
            set;
        }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date
        {
            get;
            set;
        }

        [DataType(DataType.Currency)]
        public double Price
        {
            get;
            set;
        }

        public double Litres
        {
            get;
            set;
        }
    }
}
