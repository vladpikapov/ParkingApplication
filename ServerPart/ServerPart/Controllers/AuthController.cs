using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServerPart.Data.Enums;
using ServerPart.Data.Models.AuthModels;
using ServerPart.Logic.Managers;

namespace ServerPart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private AuthManager AuthManager;

        public AuthController(AuthManager authManager) 
        {
            AuthManager = authManager;

        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Login([FromBody] Login model)
        {
            var user = AuthManager.AuthenticateUser(model.Email, model.Password);

            if (user != null)
            {
                var token = AuthManager.GenerateJWT(user);

                return Ok(new
                {
                    access_token = token
                });

            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("[action]")]
        public bool Registration([FromBody] Login model)
        {
            return AuthManager.RegistrationUser(model.Email, model.Password);
        }

      

        
    }
}
