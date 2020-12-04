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
    public class ParkingRaitingContext : IMainContext<ParkingRaiting>
    {
        private string ConnectionString;

        public ParkingRaitingContext(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void Delete(ParkingRaiting item)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ParkingRaiting> GetParkingList(int id)
        {
            string sql = $"SELECT * FROM PARKING_RAITING WHERE PARKINGID = {id}";
            var connection = new SqlConnection(ConnectionString);
            IEnumerable<ParkingRaiting> parkingRaitings = null;
            try
            {
                connection.Open();
                parkingRaitings = connection.Query<ParkingRaiting>(sql);
            }
            finally
            {
                connection.Close();
            }
            return parkingRaitings;
        }

        public IEnumerable<ParkingRaiting> GetAll()
        {
            string sql = $"SELECT * FROM PARKING_RAITING";
            var connection = new SqlConnection(ConnectionString);
            IEnumerable<ParkingRaiting> parkingRaitings = null;
            try
            {
                connection.Open();
                parkingRaitings = connection.Query<ParkingRaiting>(sql);
            }
            finally
            {
                connection.Close();
            }
            return parkingRaitings;
        }

        public void Insert(ParkingRaiting item)
        {
            string sql = $"INSERT INTO PARKING_RAITING VALUES ({item.UserId}, {item.ParkingId}, {item.UserRating})";
            var connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                connection.Query<ParkingRaiting>(sql);
            }
            finally
            {
                connection.Close();
            }
        }

        public void Update(ParkingRaiting item)
        {
            string sql = $"UPDATE PARKING_RAITING SET USERRATING = {item.UserRating} WHERE PARKINGID = {item.ParkingId} AND USERID =  {item.UserId}";
            var connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                connection.Query<ParkingRaiting>(sql);
            }
            finally
            {
                connection.Close();
            }
        }

        public ParkingRaiting Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}
