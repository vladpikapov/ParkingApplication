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

        public void Delete(int itemId)
        {
            string query = $"delete from ACCOUNTS where ID = {itemId}";
            var connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                connection.Query(query);
            }
            finally
            {
                connection.Close();
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Account Get(int id)
        {
            string query = $"SELECT * FROM ACCOUNTS WHERE ID = {id}";
            var connection = new SqlConnection(ConnectionString);
            Account account = null;
            try
            {
                connection.Open();
                account = connection.QueryFirstOrDefault<Account>(query);
            }
            finally
            {
                connection.Close();
            }
            return account;
        }

        public List<Account> GetAll()
        {
            string sql = $"SELECT * FROM ACCOUNTS";
            var connection = new SqlConnection(ConnectionString);
            List<Account> accounts = new List<Account>();
            try
            {
                connection.Open();
                accounts.AddRange(connection.Query<Account>(sql));
            }
            finally
            {
                connection.Close();
            }
            return accounts;
        }

        public void Insert(Account item)
        {

            string sql = $"INSERT INTO ACCOUNTS([LOGIN],[PASSWORD], [ROLEID], [WalletId], [Email], [CarNumber], [CreateDate]) VALUES ('{item.Login}','{CryptoHelper.GetHash(item.Password)}', 1, {item.WalletId}, '{item.Email}', '{item.CarNumber}', '{DateTime.Now}')";
            var connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                connection.Query(sql);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public void ConfirmEmail(string login)
        {
            var query = $"UPDATE ACCOUNTS SET [ConfirmEmail] = 1 where [Login] = '{login}'";
            var connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                connection.Query(query);
            }
            finally
            {
                connection.Close();
            }
        }

        public void Update(Account item)
        {
            var query = $"UPDATE ACCOUNTS SET [LOGIN] = '{item.Login}', [PASSWORD] = '{CryptoHelper.GetHash(item.Password)}', [CarNumber] = '{item.CarNumber}', [Email] = '{item.Email}', [LastLogin] = '{item.LastLogin}' WHERE [ID] = '{item.Id}'";
            var connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                connection.Query(query);
            }
            finally
            {
                connection.Close();
            }
        }

        public void UpdateColumn(string data, string column, int userId)
        {
            var query = $"UPDATE ACCOUNTS SET {column} = '{data}' where [ID] = {userId}";
            var connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                connection.Query(query);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
