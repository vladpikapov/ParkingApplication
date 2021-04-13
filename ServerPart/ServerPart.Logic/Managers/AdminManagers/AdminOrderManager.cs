using ServerPart.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerPart.Logic.Managers.AdminManagers
{
    public class AdminOrderManager : OrderManager
    {
        public AdminOrderManager(OrderContext orderContext, ParkingContext parkingContext, UserContext userContext) : base(orderContext, parkingContext, userContext)
        {
        }

    }
}
