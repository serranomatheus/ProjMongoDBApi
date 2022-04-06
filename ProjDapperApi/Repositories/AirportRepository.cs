using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using Models;
using ProjDapperApiAirport.Settings;

namespace ProjDapperApiAirport.Repositories
{
    public class AirportRepository : IAirportRepository
    {
        private string _connection;
        public AirportRepository()
        {
            _connection = DataBaseConfiguration.Get();
        }
        public bool Add(AirportData airport)
        {
            bool status = false;

            using (var db = new SqlConnection(_connection))
            {
                db.Open();
                db.Execute(AirportData.INSERT, airport);
                status = true;
            }

            return status;
        }

        public AirportData Get(string id)
        {
            using (var db = new SqlConnection(_connection))
            {
                db.Open();
                var airportData = db.QueryFirstOrDefault<AirportData>(AirportData.GET + id);
                return (AirportData)airportData;
            }
        }

        public List<AirportData> GetAll()
        {
            using (var db = new SqlConnection(_connection))
            {
                db.Open();
                var airports = db.Query<AirportData>(AirportData.GETALL);
                return (List<AirportData>)airports;
            }
        }

        public void Remove(string id)
        {
            using (var db = new SqlConnection(_connection))
            {
                db.Open();

                db.Execute(AirportData.DELETE + id);
            }
        }

        public void Update(AirportData airport)
        {
            using (var db = new SqlConnection(_connection))
            {
                db.Open();
                db.Execute(AirportData.UPDATE, airport);
            }
        }
    }
}
