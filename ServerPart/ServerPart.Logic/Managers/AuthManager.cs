using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServerPart.Data.Models.AuthModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Dapper;
using System.Linq;
using ServerPart.Data.Context;
using ServerPart.Data.Helper;
using Microsoft.Extensions.Logging;

namespace ServerPart.Logic.Managers
{
    public class AuthManager
    {
        private readonly IOptions<AuthOptions> authOptions;
        private UserContext userContext;
        //private ILogger Logger;



        public AuthManager(IOptions<AuthOptions> authOptions, UserContext userContext)
        {
            this.authOptions = authOptions;
            this.userContext = userContext;
            //Logger = logger;
        }

        public string GenerateJWT(Account user)
        {
            var authParams = authOptions.Value;

            var securityKey = authParams.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim("role", user.RoleId.ToString())
            };

            var token = new JwtSecurityToken(authParams.Issuer,
                authParams.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifetime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public Account AuthenticateUser(string email, string password)
        {

            return userContext.GetAll().FirstOrDefault(x => x.Email.Equals(email) && x.Password.Equals(CryptoHelper.GetHash(password)));

        }

        public bool RegistrationUser(string email, string password)
        {
            var user = userContext.GetAll().FirstOrDefault(x => x.Email.Equals(email));
            if (user != null)
                return false;

            try
            {
                userContext.Insert(new Account { Email = email, Password = password });
            }
            catch(Exception ex)
            {
                //Logger.LogError(ex.Message, ex);
                return false;
            }
            return true;
        }

    }
}
