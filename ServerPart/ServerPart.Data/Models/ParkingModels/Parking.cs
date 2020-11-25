using System;
using System.Collections.Generic;
using System.Text;

namespace ServerPart.Data.Models.ParkingModels
{
    public class Parking
    {
        public int Id { get; set; }

        public string Address { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longtitude { get; set; }

        public int Capacity { get; set; }

        public decimal CostPerHour { get; set; }
    }
}
