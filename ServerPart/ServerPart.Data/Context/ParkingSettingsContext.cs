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
    public class ParkingSettingsContext : IMainContext<ParkingSettings>
    {
        public string connectionString;

        public ParkingSettingsContext(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        public void Delete(int itemId)
        {
            var connection = new SqlConnection(connectionString);
            var query = $"DELETE FROM PARKING_SETTINGS WHERE PARKINGID = {itemId}";
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

        public ParkingSettings Get(int id)
        {
            var connection = new SqlConnection(connectionString);
            var query = $"SELECT * FROM PARKING_SETTINGS WHERE PARKINGID = {id}";
            ParkingSettings parkingSettings = null;
            try
            {
                connection.Open();
                parkingSettings = connection.QueryFirstOrDefault<ParkingSettings>(query);
            }
            finally
            {
                connection.Close();
            }
            return parkingSettings;
        }

        public List<ParkingSettings> GetAll()
        {
            var connection = new SqlConnection(connectionString);
            var query = "SELECT * FROM PARKING_SETTINGS";
            List<ParkingSettings> parkingSettings = new List<ParkingSettings>();
            try
            {
                connection.Open();
                parkingSettings.AddRange(connection.Query<ParkingSettings>(query));
            }
            finally
            {
                connection.Close();
            }
            return parkingSettings;
        }

        public void Insert(ParkingSettings item)
        {
            var connection = new SqlConnection(connectionString);
            var query = $"INSERT INTO PARKING_SETTINGS(PARKINGID, [ForPeopleWithDisabilities],[AllTimeService],[CCTV],[LeaveTheCarKeys]) VALUES ({item.ParkingId}, {item.ForPeopleWithDisabilities}, {item.AllTimeService}, {item.CCTV}, {item.LeaveTheCarKeys})";
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

        public void Update(ParkingSettings item)
        {
            var connection = new SqlConnection(connectionString);
            var query = $"UPDATE PARKING_SETTINGS set FORPEOPLEWITHDISABILITIES = {item.ForPeopleWithDisabilities}, ALLTIMESERVICE = {item.AllTimeService}, CCTV = {item.CCTV}, LEAVETHECARKEYS = {item.LeaveTheCarKeys} where PARKINGID = {item.ParkingId}";
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
