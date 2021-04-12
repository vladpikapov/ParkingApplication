using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerPart.Controllers.DIspatcher
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "3")]
    public class DOrderController: ControllerBase
    {
    }
}
