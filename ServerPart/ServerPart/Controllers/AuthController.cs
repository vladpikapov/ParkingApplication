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
        private AccountManager AccountManager;

        public AuthController(AuthManager authManager, AccountManager accountManager) 
        {
            AuthManager = authManager;
            AccountManager = accountManager;

        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            var user = AuthManager.AuthenticateUser(model.Login, model.Password);

            if (user != null)
            {
                var token = AuthManager.GenerateJWT(user);
                AccountManager.UpdateUserData(user, 5);
                return Ok(new
                {
                    access_token = token,
                    confirm_email = user.ConfirmEmail
                });

            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("[action]")]
        public bool Registration([FromBody] LoginModel model)
        {
            return AuthManager.RegistrationUser(model);
        }

      
      

        

        
    }
}
