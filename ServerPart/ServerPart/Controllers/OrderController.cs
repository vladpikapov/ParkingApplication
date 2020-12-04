using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerPart.Data.Models.ParkingModels;
using ServerPart.Logic.Managers;

namespace ServerPart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private OrderManager OrderManager;

        private int UserId => int.Parse(User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value);

        public OrderController(OrderManager orderManager)
        {
            OrderManager = orderManager;
        }

        [HttpGet("[action]")]
        public IEnumerable<Order> GetOrders()
        {
            return OrderManager.GetUserHistoryOrders(UserId);
        }

        [HttpPost("[action]")]
        public void PostOrder([FromBody] Order order)
        {
            order.OrderUserId = UserId;
            OrderManager.InsertOrder(order);
        }

        [HttpPut("[action]")]
        public void UpdateOrder([FromBody] Order order)
        {
            OrderManager.UpdateOrder(order);
        }
    }
}
