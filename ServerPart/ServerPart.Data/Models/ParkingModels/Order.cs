using System;
using System.Collections.Generic;
using System.Text;

namespace ServerPart.Data.Models.ParkingModels
{
    public class Order
    {
        public int OrderId { get; set; }

        public DateTime OrderStartDate { get; set; }

        public DateTime OrderEndDate { get; set; }

        public int UserId { get; set; }

        public int ParkingId { get; set; }
    }
}
