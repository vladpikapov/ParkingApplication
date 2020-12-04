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
        private ParkingContext ParkingContext;
        private ParkingRaitingContext prContext;
        private OrderContext OrderContext;
        private OrderManager oManager;

        public ParkingManager(ParkingContext parkingContext, OrderContext orderContext, ParkingRaitingContext parkingRaitingContext, OrderManager orderManager)
        {
            ParkingContext = parkingContext;
            OrderContext = orderContext;
            prContext = parkingRaitingContext;
            oManager = orderManager;
        }

        public IEnumerable<Parking> GetParkings()
        {
            var dateNow = DateTime.Now;
            var parkingsIdOrder = OrderContext.GetAll().Where(x => x.OrderStartDate <= dateNow && dateNow <= x.OrderEndDate ).Select(x => x.OrderParkingId);
            var allParkings = ParkingContext.GetAll();
            allParkings.ToList().ForEach(x =>
            {
                x.ParkingRaiting = GetParkingRaiting(x.Id);
            });
            if (parkingsIdOrder != null && parkingsIdOrder.Any())
            {
                foreach (var parking in allParkings)
                {
                    
                    if (parkingsIdOrder.Contains(parking.Id))
                    {
                        parking.Capacity--;
                    }
                }
            }
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

        public void SetParkingRaiting(ParkingRaiting raiting)
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
