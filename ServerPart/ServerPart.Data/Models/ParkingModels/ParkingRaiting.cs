using System;
using System.Collections.Generic;
using System.Text;

namespace ServerPart.Data.Models.ParkingModels
{
    class ParkingRaiting
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int ParkingId { get; set; }

        public int UserRaiting { get; set; }
    }
}
