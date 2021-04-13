using ServerPart.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerPart.Logic.Managers.DispatcherManagers
{
    public class DispatcherOrderManager : OrderManager
    {
        public DispatcherOrderManager(OrderContext orderContext, ParkingContext parkingContext, UserContext userContext) : base(orderContext, parkingContext, userContext)
        {
        }
    }
}
