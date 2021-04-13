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
        protected OrderContext OrderContext;
        protected ParkingContext ParkingContext;
        protected UserContext UserContext;

        public OrderManager(OrderContext orderContext, ParkingContext parkingContext, UserContext userContext)
        {
            OrderContext = orderContext;
            ParkingContext = parkingContext;
            UserContext = userContext;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return OrderContext.GetAll();
        }

        public IEnumerable<Order> GetUserHistoryOrders(int userId)
        {
            var orders = OrderContext.GetAll().Where(x => x.OrderUserId == userId);
            var parkings = ParkingContext.GetAll();
            foreach(var order in orders)
            {
                order.Account = UserContext.Get(userId);
                order.Parking = parkings.FirstOrDefault(x => x.Id == order.OrderParkingId);
                order.OrderStartDate = order.OrderStartDate.ToLocalTime();
                order.OrderEndDate = order.OrderEndDate.ToLocalTime();
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

        public void DeleteOrder(int orderId)
        {
            OrderContext.Delete(orderId);
        }

        public Order GetOrder(int orderId)
        {
            var order = OrderContext.Get(orderId);
            order.Account = UserContext.Get(order.OrderUserId);
            order.Parking = ParkingContext.Get(order.OrderParkingId);
            order.OrderStartDate = order.OrderStartDate.ToLocalTime();
            order.OrderEndDate = order.OrderEndDate.ToLocalTime();
            return order;
        }
    }
}
