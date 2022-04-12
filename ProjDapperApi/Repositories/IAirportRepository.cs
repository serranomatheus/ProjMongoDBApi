using System.Collections.Generic;
using Models;

namespace ProjDapperApiAirport.Repositories
{
    public interface IAirportRepository
    {
        bool Add(AirportData airport);

        List<AirportData> GetAll();
        void Remove(string id);
        AirportData Get(string id);

        void Update(AirportData airport);

        AirportData GetCode(string code);
    }
}
