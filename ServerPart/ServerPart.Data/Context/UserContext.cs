using Microsoft.Extensions.Configuration;
using ServerPart.Data.Interfaces;
using ServerPart.Data.Models.AuthModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using System.Text;
using ServerPart.Data.Helper;

namespace ServerPart.Data.Context
{
    public class UserContext : IMainContext<Account>
    {
        private string ConnectionString;

        public UserContext(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void Delete(Account item)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Account Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Account> GetAll()
        {
            string sql = $"SELECT * FROM ACCOUNTS";
            var connection = new SqlConnection(ConnectionString);
            IEnumerable<Account> accounts = null;
            try
            {
                connection.Open();
                accounts = connection.Query<Account>(sql);
            }
            finally
            {
                connection.Close();
            }
            return accounts;
        }

        public void Insert(Account item)
        {

            string sql = $"INSERT INTO ACCOUNTS VALUES ('{item.Email}','{CryptoHelper.GetHash(item.Password)}', 1)";
            var connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                connection.Query(sql);
            }
            catch(Exception ex)
            {
                throw new Exception();
            }
            finally
            {
                connection.Close();
            }
        }

        public void Update(Account item)
        {
            throw new NotImplementedException();
        }
    }
}
