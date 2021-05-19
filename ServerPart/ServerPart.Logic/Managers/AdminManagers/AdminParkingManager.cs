using ServerPart.Data.Context;
using ServerPart.Data.Models.ParkingModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
            if (parking.Address != null)
            {
                ParkingContext.Insert(parking);
                var parkingId = ParkingContext.GetAll().Last().Id;
                parking.ParkingSettings.ParkingId = parkingId;
                psContext.Insert(parking.ParkingSettings);
            }
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
            OrderContext.GetAll().Where(x => x.OrderParkingId == parkingId).ToList().ForEach(x =>
            {
                OrderContext.Delete(x.OrderId);
            });
            psContext.Delete(parkingId);
            prContext.Delete(parkingId);
            ParkingContext.Delete(parkingId);
        }

        public bool CanDeleteParking(int parkingId)
        {
            return !oManager.GetAllOrders().Where(x => x.OrderParkingId == parkingId).Any();

        }
    }
}
