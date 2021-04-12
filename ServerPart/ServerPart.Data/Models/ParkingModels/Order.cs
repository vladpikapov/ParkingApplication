using ServerPart.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ServerPart.Data.Models.ParkingModels
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime OrderStartDate { get; set; }

        public DateTime OrderEndDate { get; set; }

        public OrderEnum OrderStatus { get; set; }

        public int OrderUserId { get; set; }

        public int OrderParkingId { get; set; }

        public Parking Parking { get; set; }

    }
}
