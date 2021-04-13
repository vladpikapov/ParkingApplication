using ServerPart.Data.Context;
using ServerPart.Data.Models.ParkingModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerPart.Logic.Managers.AdminManagers
{
    public class AdminParkingManager : ParkingManager
    {

        public AdminParkingManager(ParkingContext parkingContext, OrderContext orderContext, ParkingRaitingContext parkingRaitingContext, OrderManager orderManager, ParkingSettingsContext parkingSettingsContext) : base(parkingContext, orderContext, parkingRaitingContext, orderManager, parkingSettingsContext)
        {
        }

        public void CreateParking(Parking parking)
        {
            ParkingContext.Insert(parking);
            psContext.Insert(parking.ParkingSettings);
        }

        public void UpdateParking(Parking parking)
        {
            psContext.Update(parking.ParkingSettings);
            ParkingContext.Update(parking);
        }

        public Parking GetParking(int parkingId)
        {   
            var parking = ParkingContext.Get(parkingId);
            parking.ParkingRaiting = GetParkingRaiting(parkingId);
            parking.ParkingSettings = psContext.Get(parkingId);
            return parking;
        }

        public void DeleteParking(int parkingId)
        {
            psContext.Delete(parkingId);
            ParkingContext.Delete(parkingId);
        }
    }
}
