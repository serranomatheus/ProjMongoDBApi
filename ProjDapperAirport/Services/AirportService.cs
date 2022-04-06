using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using ProjDapperAirport.Repositories;

namespace ProjDapperAirport.Services
{

    public class AirportService
    {
        private IAirportRepository _airportRepository;

        public AirportService()
        {
            _airportRepository = new AirportRepository();
        }

        public bool Add(AirportData airport)
        {
            return _airportRepository.Add(airport);
        }
        public List<AirportData> GetAll()
        {
            return _airportRepository.GetAll();
        }



    }
}
