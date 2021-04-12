using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerPart.Data.Models.ParkingModels;
using ServerPart.Logic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerPart.Controllers.Administator
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "2")]
    public class AOrderController: ControllerBase
    {
        OrderManager m_OrderManager;

        public AOrderController(OrderManager orderManager)
        {
            m_OrderManager = orderManager;
        }

        [HttpGet]
        public IEnumerable<Order> GetOrders()
        {
            return m_OrderManager.GetAllOrders();
        }

        [HttpPut]
        public void EditOrder()
        {

        }
    }
}
