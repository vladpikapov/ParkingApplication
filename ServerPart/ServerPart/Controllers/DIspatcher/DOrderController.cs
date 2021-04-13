using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerPart.Data.Models.ParkingModels;
using ServerPart.Logic.Managers.DispatcherManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerPart.Controllers.DIspatcher
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Dispatcher")]
    public class DOrderController: ControllerBase
    {
        private DispatcherOrderManager dispatcherOrderManager;

        public DOrderController(DispatcherOrderManager orderManager)
        {
            dispatcherOrderManager = orderManager;
        }

        [HttpGet("[action]/{userId}")]
        public IEnumerable<Order> GetOrders([FromRoute]int userId)
        {
            return dispatcherOrderManager.GetAllOrders().Where(x => x.OrderUserId == userId);
        }

        [HttpPost("[action]")]
        public void PostOrder([FromBody] Order order)
        {
            dispatcherOrderManager.InsertOrder(order);
        }

        [HttpPut("[action]")]
        public void UpdateOrder([FromBody] Order order)
        {
            dispatcherOrderManager.UpdateOrder(order);
        }

        [HttpDelete("[action]/{orderId}")]
        public void DeleteOrder([FromRoute] int orderId)
        {
            dispatcherOrderManager.DeleteOrder(orderId);
        }
    }
}
