using Microsoft.Extensions.Configuration;
using ServerPart.Data.Interfaces;
using ServerPart.Data.Models.ParkingModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using System.Text;
using ServerPart.Data.Helper;

namespace ServerPart.Data.Context
{
    public class OrderContext : IMainContext<Order>
    {
        public string connectionString;

        public OrderContext(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void Delete(int itemId)
        {
            if (!string.IsNullOrEmpty(connectionString))
            {
                SqlConnection connection = new SqlConnection(connectionString);
                try
                {
                    var query = $"DELETE FROM [dbo].[ORDERS] WHERE OrderId = {itemId}";
                    connection.Open();
                    connection.Query<Order>(query);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Order Get(int id)
        {

            SqlConnection connection = new SqlConnection(connectionString);
            Order order = null;
            try
            {
                var query = $"SELECT * FROM ORDERS WHERE OrderId = {id}";
                connection.Open();
                order = connection.QueryFirstOrDefault<Order>(query);
            }
            finally
            {
                connection.Close();
            }
            return order;
        }

        public List<Order> GetAll()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            List<Order> orders = new List<Order>();
            try
            {
                connection.Open();
                orders.AddRange(connection.Query<Order>(QueryManager.GetQueryForSelect("[dbo].ORDERS")));
            }
            finally
            {
                connection.Close();
            }
            return orders;
        }

        public void Insert(Order item)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string query = $"INSERT INTO [dbo].ORDERS(OrderStartDate, OrderEndDate, OrderUserId, OrderParkingId) values ('{item.OrderStartDate}', '{item.OrderEndDate}', {item.OrderUserId}, {item.OrderParkingId})";
                connection.Query(query);
            }
            finally
            {
                connection.Close();
            }
        }

        public void Update(Order item)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string query = $"UPDATE [dbo].ORDERS set OrderStartDate = '{item.OrderStartDate}', OrderEndDate = '{item.OrderEndDate}', OrderParkingId = {item.OrderParkingId} where OrderId = {item.OrderId}";
                connection.Query<Order>(query);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
