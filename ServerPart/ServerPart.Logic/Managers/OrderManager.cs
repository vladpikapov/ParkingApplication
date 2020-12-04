using ServerPart.Data.Context;
using ServerPart.Data.Models.ParkingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServerPart.Logic.Managers
{
    public class OrderManager
    {
        private OrderContext OrderContext;
        private ParkingContext ParkingContext;

        public OrderManager(OrderContext orderContext, ParkingContext parkingContext)
        {
            OrderContext = orderContext;
            ParkingContext = parkingContext;
        }

        public IEnumerable<Order> GetUserHistoryOrders(int userId)
        {
            var orders = OrderContext.GetAll().Where(x => x.OrderUserId == userId);
            var parkings = ParkingContext.GetAll();
            foreach(var order in orders)
            {
                order.Parking = parkings.FirstOrDefault(x => x.Id == order.OrderParkingId);
            }
            return orders;
        }

        public void InsertOrder(Order order)
        {
            OrderContext.Insert(order);
        }

        public void UpdateOrder(Order order)
        {
            OrderContext.Update(order);
        }
    }
}
