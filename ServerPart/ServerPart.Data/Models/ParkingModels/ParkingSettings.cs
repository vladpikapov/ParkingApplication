using System;
using System.Collections.Generic;
using System.Text;

namespace ServerPart.Data.Models.ParkingModels
{
    public class ParkingSettings
    {
        public int ParkingId { get; set; }
        public int ForPeopleWithDisabilities { get; set; }
        public int AllTimeService { get; set; }
        public int CCTV { get; set; }
        public int LeaveTheCarKeys { get; set; }
    }
}
