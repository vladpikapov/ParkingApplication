using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerPart.Data.Models.ParkingModels;
using ServerPart.Logic.Managers;
using ServerPart.Logic.Managers.AdminManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerPart.Controllers.Administator
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AOrderController : ControllerBase
    {
        OrderManager m_OrderManager;

        public AOrderController(AdminOrderManager orderManager)
        {
            m_OrderManager = orderManager;
        }

        [HttpGet("[action]/{userId}")]
        public IEnumerable<Order> GetUserOrders([FromRoute] int userId)
        {
            return m_OrderManager.GetAllOrders().Where(x => x.OrderUserId == userId);
        }

        [HttpPut("[action]")]
        public void UpdateOrder(Order order)
        {
            m_OrderManager.UpdateOrder(order);
        }

        [HttpPost("[action]")]
        public void PostOrder(Order order)
        {
            m_OrderManager.InsertOrder(order);
        }

        [HttpGet("[action]/{orderId}")]
        public Order GetOrder([FromRoute] int orderId)
        {
            return m_OrderManager.GetOrder(orderId);
        }

        [HttpDelete("[action]/{orderId}")]
        public void DeleteOrder([FromRoute]int orderId)
        {
            m_OrderManager.DeleteOrder(orderId);
        }
    }
}
