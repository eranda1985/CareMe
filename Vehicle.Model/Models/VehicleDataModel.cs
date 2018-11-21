using NPoco;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicle.Model.Models
{
    [TableName("VehicleData")]
    public class  VehicleDataModel
    {
        [ResultColumn("Id")]
        public long Id { get; set; }
        [Column("Type")]
        public string  VehicleType { get; set; }
        [Column("Brand")]
        public string Brand { get; set; }
        [Column("Model")]
        public string Model { get; set; }
        [Column("FuelType")]
        public string FuelType { get; set; }
        [Column("RegoPlate")]
        public string RegoPlate { get; set; }
        [Column("Date")]
        public DateTime Date { get; set; }
        [Column("ODOMeter")]
        public double ODOMeter { get; set; }
    }
}
