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

        public void Delete(Order item)
        {
            if (!string.IsNullOrEmpty(connectionString))
            {
                SqlConnection connection = new SqlConnection(connectionString);
                try
                {
                    connection.Open();
                    connection.Query<Order>(QueryManager.GetQueryForDelete("[dbo].ORDERS", item.Id));
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
                connection.Open();
                order = connection.QueryFirstOrDefault<Order>(QueryManager.GetQueryForSelect("[dbo].ORDERS", id));
            }
            finally
            {
                connection.Close();
            }
            return order;
        }

        public IEnumerable<Order> GetAll()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            IEnumerable<Order> orders = null;
            try
            {
                connection.Open();
                orders = connection.Query<Order>(QueryManager.GetQueryForSelect("[dbo].ORDERS"));
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
                string query = $"INSERT INTO [dbo].ORDERS values ('{item.OrderStartDate:yyyy-MM-dd HH:mm:ss}', '{item.OrderEndDate:yyyy-MM-dd HH:mm:ss}', {item.OrderUserId}, {item.OrderParkingId})";
                connection.Query<Order>(query);
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
                string query = $"UPDATE [dbo].ORDERS set ORDERSTARTDATE = '{item.OrderStartDate:yyyy-MM-dd HH:mm:ss}', ORDERENDDATE = '{item.OrderEndDate:yyyy-MM-dd HH:mm:ss}', ORDERPARKINGID = {item.OrderParkingId} where ID = {item.Id}";
                connection.Query<Order>(query);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
