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
                var connection = new SqlConnection(connectionString);
                try
                {
                    connection.Open();
                    connection.Query<Order>(QueryManager.GetQueryForDelete("[dbo].PARKING", itemId));
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
            var connection = new SqlConnection(connectionString);
            Parking parking = null;
            try
            {
                connection.Open();
                parking = connection.QueryFirstOrDefault<Parking>(QueryManager.GetQueryForSelect("[dbo].PARKING", id));
            }
            finally
            {
                connection.Close();
            }
            return parking;
        }

        public List<Parking> GetAll()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            List<Parking> parkings = new List<Parking>();
            try
            {
                connection.Open();
                parkings.AddRange(connection.Query<Parking>(QueryManager.GetQueryForSelect("[dbo].PARKING")));
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
                string query = $"INSERT INTO [dbo].PARKING([Address],[Latitude],[Longitude],[Capacity],[CostPerHour]) values ('{item.Address}', '{item.Latitude}', '{item.Longitude}', {item.Capacity}, {item.CostPerHour.ToString().Replace(',', '.')})";
                connection.Query(query);
            }
            finally
            {
                connection.Close();
            }
        }

        public void Update(Parking item)
        {
            var connection = new SqlConnection(connectionString);
            try
            {
                var query = $"UPDATE [dbo].PARKING set [ADDRESS] = '{item.Address}', LATITUDE = '{item.Latitude}', LONGITUDE = '{item.Longitude}', CAPACITY = {item.Capacity}, CostPerHour = {item.CostPerHour.ToString().Replace(',', '.')} WHERE [ID] = {item.Id}";
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
