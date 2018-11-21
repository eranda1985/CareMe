using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicle.Model.Dto
{
    public class VehicleDataDto
    {
        public long Id { get; set; }
        public string VehicleType { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string FuelType { get; set; }
        public string RegoPlate { get; set; }
        public DateTime Date { get; set; }
        public double ODOMeter { get; set; }
    }
}
