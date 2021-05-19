using ServerPart.Data.Context;
using ServerPart.Data.Models.ParkingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServerPart.Logic.Managers
{
    public class ParkingManager
    {
        protected ParkingContext ParkingContext;
        protected ParkingRaitingContext prContext;
        protected OrderContext OrderContext;
        protected OrderManager oManager;
        protected ParkingSettingsContext psContext;

        public ParkingManager(ParkingContext parkingContext, OrderContext orderContext, ParkingRaitingContext parkingRaitingContext, OrderManager orderManager, ParkingSettingsContext parkingSettingsContext)
        {
            ParkingContext = parkingContext;
            OrderContext = orderContext;
            prContext = parkingRaitingContext;
            oManager = orderManager;
            psContext = parkingSettingsContext;
        }

        public IEnumerable<Parking> GetParkings()
        {
            var allParkings = ParkingContext.GetAll();
            var allOrders = OrderContext.GetAll().ToList();
            allParkings.ToList().ForEach(x =>
            {
                x.ParkingRaiting = GetParkingRaiting(x.Id);
                x.ParkingSettings = psContext.Get(x.Id);
                x.FreePlaces = GetParkingFreePlaces(x, allOrders);
            });
            return allParkings;
        }

        public double GetParkingRaiting(int parkingId)
        {
            var allParkingRates = prContext.GetParkingList(parkingId);
            int pCount = allParkingRates.ToList().Count;
            if (allParkingRates.Any())
            {
                return allParkingRates.Select(x => x.UserRating).Sum() / (double)pCount;
            }
            return 0;
        }

        public void SetParkingRaiting(ParkingRating raiting)
        {
            var checkOrder = prContext.GetAll().FirstOrDefault(x => x.UserId == raiting.UserId && x.ParkingId == raiting.ParkingId);
            if(checkOrder != null)
            {
                prContext.Update(raiting);
            }
            else
            {
                prContext.Insert(raiting);
            }
        }

        public int GetParkingFreePlaces(Parking parking, List<Order> orders)
        {
            var actualOrders = orders.Where(x => x.OrderParkingId == parking.Id && x.OrderEndDate >= DateTime.Now);
            return parking.Capacity - actualOrders.Count();
        }

        public IEnumerable<Parking> GetUserHistoryParking(int userId)
        {
            var parkingsIds = oManager.GetUserHistoryOrders(userId).Select(x => x.OrderParkingId);
            var allParkings = ParkingContext.GetAll();
            IEnumerable<Parking> userParkings = new List<Parking>();
            allParkings.ToList().ForEach(x =>
            {
                if (parkingsIds.Contains(x.Id))
                {
                    userParkings.ToList().Add(x);
                }
            });
            return userParkings;
        }
    }
}
