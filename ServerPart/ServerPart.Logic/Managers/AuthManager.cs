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
        private WalletContext walletContext;
        private MailManager mailManager;
        //private ILogger Logger;



        public AuthManager(IOptions<AuthOptions> authOptions, UserContext userContext, WalletContext walletContext, MailManager mailManager)
        {
            this.authOptions = authOptions;
            this.userContext = userContext;
            this.walletContext = walletContext;
            this.mailManager = mailManager;
            //Logger = logger;
        }

        public string GenerateJWT(Account user)
        {
            var authParams = authOptions.Value;

            var securityKey = authParams.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Login),
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

            return userContext.GetAll().FirstOrDefault(x =>x.Login.Equals(email) && x.Password.Equals(CryptoHelper.GetHash(password)));

        }

        public bool RegistrationUser(LoginModel model)
        {
            var user = userContext.GetAll().FirstOrDefault(x => x.Login.Equals(model.Login));
            if (user != null)
                return false;

            try
            {
                walletContext.Insert(new Wallet());
                var getLastWallet = walletContext.GetAll().Last();
                userContext.Insert(new Account { Login = model.Login, Password = model.Password, WalletId = getLastWallet.Id, Email = model.Email, CarNumber = model.CarNumber });
                mailManager.SendCodeToMail(model.Email);
            }
            catch(Exception ex)
            {
                return false;
            }
            return true;
        }

    }
}
