using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ServerPart.Data.Models.ParkingModels
{
    public class Parking
    {
        public int Id { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public int Capacity { get; set; }

        public decimal CostPerHour { get; set; }

        public double ParkingRaiting { get; set; }
    }
}
