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

namespace ServerPart.Logic.Managers
{
    public class AuthManager
    {
        private readonly IOptions<AuthOptions> authOptions;

        private string ConnectionString { get; set; }

        public AuthManager(IOptions<AuthOptions> authOptions, IConfiguration configuration)
        {
            this.authOptions = authOptions;
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
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
                new Claim("role", user.Role.ToString())
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
            string sql = $"SELECT * FROM ACCOUNTS WHERE EMAIL = '{email}' AND PASSWORD = '{password}'";
            var connection = new SqlConnection(ConnectionString);
            Account account = null;
            try
            {
                connection.Open();
                account = connection.Query<Account>(sql).FirstOrDefault();
            }
            finally
            {
                connection.Close();
            }
            return account;

        }
    }
}
