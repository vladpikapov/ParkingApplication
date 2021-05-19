using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerPart.Data.Models.ParkingModels;
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
    public class AParkingController: ControllerBase
    {
        private AdminParkingManager apManager;

        public AParkingController(AdminParkingManager adminParkingManager)
        {
            apManager = adminParkingManager;
        }

        [HttpGet("[action]")]
        public IEnumerable<Parking> GetParkings()
        {
            return apManager.GetParkings();
        }

        [HttpGet("[action]/{parkingId}")]
        public Parking GetParking([FromRoute] int parkingId)
        {
            return apManager.GetParking(parkingId);
        }

        [HttpPost("[action]")]
        public void CreateParking([FromBody]Parking parking)
        {
            apManager.CreateParking(parking);
        }

        [HttpPut("[action]")]
        public void UpdateParking(Parking parking)
        {
            apManager.UpdateParking(parking);
        }

        [HttpDelete("[action]/{parkingId}")]
        public void DeleteParking([FromRoute]int parkingId)
        {
            apManager.DeleteParking(parkingId);
        }

        [HttpGet("[action]/{parkingId}")]
        public bool CanDeleteParking([FromRoute]int parkingId)
        {
            return apManager.CanDeleteParking(parkingId);
        }
    }
}
