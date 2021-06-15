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
        protected WalletContext WalletContext;
        protected MailManager MailManager;

        public OrderManager(OrderContext orderContext, ParkingContext parkingContext, UserContext userContext, WalletContext walletContext, MailManager mailManager)
        {
            OrderContext = orderContext;
            ParkingContext = parkingContext;
            UserContext = userContext;
            WalletContext = walletContext;
            MailManager = mailManager;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            var userList = UserContext.GetAll();
            var parkingList = ParkingContext.GetAll();
            var orders = OrderContext.GetAll();
            foreach(var order in orders)
            {
                var user = userList.FirstOrDefault(x => x.Id == order.OrderUserId);
                if(user != null)
                {
                    order.Account = user;
                }
                var parking = parkingList.FirstOrDefault(x => x.Id == order.OrderParkingId);
                if(parking != null)
                {
                    order.Parking = parking;
                }
            }
            return orders;
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
            var userWallet = WalletContext.Get(order.OrderUserId);
            userWallet.MoneySum -= order.AllCost;
            WalletContext.Update(userWallet);
            OrderContext.Insert(order);
            order.Account = UserContext.Get(order.OrderUserId);
            order.Parking = ParkingContext.Get(order.OrderParkingId);
            MailManager.SendOrderToMail(order.Account.Email, order);
            

        }

        public void UpdateOrder(Order order)
        {
            OrderContext.Update(order);
            MailManager.SendOrderToMail(order.Account.Email, order);
        }

        public void DeleteOrder(int orderId)
        {
            var order = OrderContext.Get(orderId);
            var user = UserContext.Get(order.OrderUserId);
            var parking = ParkingContext.Get(order.OrderParkingId);
            order.Account = user;
            order.Parking = parking;
            OrderContext.Delete(orderId);
            MailManager.SendDeleteOrderToMail(user.Email, order);
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
