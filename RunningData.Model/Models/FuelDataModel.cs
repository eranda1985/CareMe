using System;
using NPoco;

namespace RunningData.Model.Models
{
    [TableName("FuelData")]
    public class FuelDataModel
    {
        [Column("Date")]
        public DateTime Date { get; set; }

        [Column("Litres")]
        public double Litres { get; set; }

        [Column("Price")]
        public double Price { get; set; }

        [Column("Mileage")]
        public double Mileage { get; set; }
    }
}
