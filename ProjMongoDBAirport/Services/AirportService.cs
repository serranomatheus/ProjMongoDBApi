using System.Collections.Generic;
using Models;
using MongoDB.Driver;
using ProjMongoDBApi.Utils;

namespace ProjMongoDBApi.Services
{
    public class AirportService

    {
        private readonly IMongoCollection<Airport> _airports;        

        public AirportService(IProjMongoDBApiSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _airports = database.GetCollection<Airport>(settings.AirportCollectionName);
        }

        public List<Airport> Get() =>
            _airports.Find(airport => true).ToList();

        public Airport Get(string id) =>
            _airports.Find<Airport>(airport => airport.Id == id).FirstOrDefault();

        public Airport Create(Airport airport)
        {
            _airports.InsertOne(airport);
            return airport;
        }

        public void Update(string id, Airport airportIn) =>
            _airports.ReplaceOne(airport => airport.Id == id, airportIn);

        public void Remove(Airport airportIn) =>
            _airports.DeleteOne(airport => airport.Id == airportIn.Id);

        public void Remove(string id) =>
            _airports.DeleteOne(airport => airport.Id == id);
    }
}
