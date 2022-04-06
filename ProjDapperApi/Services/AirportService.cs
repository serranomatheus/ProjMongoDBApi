using System.Collections.Generic;
using Models;
using ProjDapperApiAirport.Repositories;

namespace ProjDapperApiAirport.Services
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

        public AirportData Get(string id)
        {
            return _airportRepository.Get(id);
        }
        public void Remove(string id)
        {
           _airportRepository.Remove(id);
        }
       
        public void UpDate(AirportData airportData)
        {
            _airportRepository.Update(airportData);
        }

    }
}
