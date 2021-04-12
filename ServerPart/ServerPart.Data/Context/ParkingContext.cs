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
    public class ParkingContext : IMainContext<Parking>
    {
        public string connectionString;

        public ParkingContext(IConfiguration configuration)
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
                    connection.Open();
                    connection.Query<Order>(QueryManager.GetQueryForDelete("[dbo].PARKINGS", itemId));
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

        public Parking Get(int id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            Parking parking = null;
            try
            {
                connection.Open();
                parking = connection.QueryFirstOrDefault<Parking>(QueryManager.GetQueryForSelect("[dbo].PARKINGS", id));
            }
            finally
            {
                connection.Close();
            }
            return parking;
        }

        public IEnumerable<Parking> GetAll()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            IEnumerable<Parking> parkings = null;
            try
            {
                connection.Open();
                parkings = connection.Query<Parking>(QueryManager.GetQueryForSelect("[dbo].PARKINGS"));
            }
            finally
            {
                connection.Close();
            }
            return parkings;
        }

        public void Insert(Parking item)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string query = $"INSERT INTO [dbo].PARKING values ('{item.Address}', {item.Latitude}, {item.Longitude}, {item.Capacity}, {item.CostPerHour}, '{item.City}')";
                connection.Query<Order>(query);
            }
            finally
            {
                connection.Close();
            }
        }

        public void Update(Parking item)
        {
            throw new NotImplementedException();
        }
    }
}
