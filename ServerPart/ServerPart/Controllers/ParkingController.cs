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
    public class ParkingController : ControllerBase
    {
        private ParkingManager pManager;

        private int UserId => int.Parse(User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value);

        public ParkingController(ParkingManager parkingManager)
        {
            pManager = parkingManager;
        }

        [HttpGet("[action]")]
        public IEnumerable<Parking> GetParking()
        {
            return pManager.GetParkings();
        }

        [HttpGet("[action]/{id}")]
        public double GetParkingRaiting([FromRoute] int id)
        {
            return pManager.GetParkingRaiting(id);
        }

        [HttpPost("[action]")]
        public void SetParkingRaiting([FromBody]ParkingRaiting raiting)
        {
            raiting.UserId = UserId;
            pManager.SetParkingRaiting(raiting);
        }

        [HttpGet("[action]")]
        public IEnumerable<Parking> GetUserHistoryParking()
        {
            return pManager.GetUserHistoryParking(UserId);
        }
    }
}
