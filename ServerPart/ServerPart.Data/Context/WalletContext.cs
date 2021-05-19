using Dapper;
using Microsoft.Extensions.Configuration;
using ServerPart.Data.Interfaces;
using ServerPart.Data.Models.AuthModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ServerPart.Data.Context
{
    public class WalletContext : IMainContext<Wallet>
    {

        public string connectionString;

        public WalletContext(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        public void Delete(int itemId)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Wallet Get(int id)
        {
            var query = $"SELECT * FROM WALLETS where ID = {id}";
            var connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                var wallet = connection.QueryFirstOrDefault<Wallet>(query);
                return wallet;
            }
            finally
            {
                connection.Close();
            }
            
        }

        public List<Wallet> GetAll()
        {
            var query = $"select * from WALLETS";
            var connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                var res = connection.Query<Wallet>(query).AsList();
                return res;
            }
            finally
            {
                connection.Close();
            }
        }

        public void Insert(Wallet item)
        {
            var query = $"INSERT INTO WALLETS values (0.0)";
            var connection = new SqlConnection(connectionString);
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

        public void Update(Wallet item)
        {
            var query = $"UPDATE WALLETS set MoneySum = {item.MoneySum.ToString().Replace(',','.')} WHERE Id = {item.Id}";
            var connection = new SqlConnection(connectionString);
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
