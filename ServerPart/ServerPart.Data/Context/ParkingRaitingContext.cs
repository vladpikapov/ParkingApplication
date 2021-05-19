using Dapper;
using Microsoft.Extensions.Configuration;
using ServerPart.Data.Interfaces;
using ServerPart.Data.Models.ParkingModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ServerPart.Data.Context
{
    public class ParkingRaitingContext : IMainContext<ParkingRating>
    {
        private string ConnectionString;

        public ParkingRaitingContext(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void Delete(int itemId)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                var query = $"DELETE FROM [dbo].[PARKING_RATING] WHERE ParkingId = {itemId}";
                connection.Open();
                connection.Query(query);
            }
            finally
            {
                connection.Close();
            }
        }

        public void DeleteByUser(int itemId)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                var query = $"DELETE FROM [dbo].[PARKING_RATING] WHERE UserId = {itemId}";
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

        public IEnumerable<ParkingRating> GetParkingList(int id)
        {
            string sql = $"SELECT * FROM PARKING_RATING WHERE ParkingId = {id}";
            var connection = new SqlConnection(ConnectionString);
            IEnumerable<ParkingRating> parkingRaitings = null;
            try
            {
                connection.Open();
                parkingRaitings = connection.Query<ParkingRating>(sql);
            }
            finally
            {
                connection.Close();
            }
            return parkingRaitings;
        }

        public List<ParkingRating> GetAll()
        {
            string sql = $"SELECT * FROM PARKING_RATING";
            var connection = new SqlConnection(ConnectionString);
            List<ParkingRating> parkingRaitings = new List<ParkingRating>();
            try
            {
                connection.Open();
                parkingRaitings.AddRange(connection.Query<ParkingRating>(sql));
            }
            finally
            {
                connection.Close();
            }
            return parkingRaitings;
        }

        public void Insert(ParkingRating item)
        {
            string sql = $"INSERT INTO PARKING_RATING VALUES ({item.UserId}, {item.ParkingId}, {item.UserRating})";
            var connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                connection.Query<ParkingRating>(sql);
            }
            finally
            {
                connection.Close();
            }
        }

        public void Update(ParkingRating item)
        {
            string sql = $"UPDATE PARKING_RATING SET USERRATING = {item.UserRating} WHERE PARKINGID = {item.ParkingId} AND USERID =  {item.UserId}";
            var connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                connection.Query<ParkingRating>(sql);
            }
            finally
            {
                connection.Close();
            }
        }

        public ParkingRating Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}
