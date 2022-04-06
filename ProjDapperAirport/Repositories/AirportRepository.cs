using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Models;
using ProjDapperAirport.Config;

namespace ProjDapperAirport.Repositories
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

        public List<AirportData> GetAll()
        {
            using (var db = new SqlConnection(_connection))
            {
                db.Open();
                var airports = db.Query<AirportData>(AirportData.GETALL);
                return (List<AirportData>)airports;
            }
        }
    }
}
