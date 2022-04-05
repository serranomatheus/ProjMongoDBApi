using System.Collections.Generic;
using Models;
using MongoDB.Driver;
using ProjMongoDBApi.Utils;

namespace ProjMongoDBApi.Services
{
    public class FlightService

    {
        private readonly IMongoCollection<Airport> _flights;        

        public FlightService(IProjMongoDBApiSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _flights = database.GetCollection<Airport>(settings.FlightCollectionName);
        }

        public List<Airport> Get() =>
            _flights.Find(flight => true).ToList();

        public Airport Get(string id) =>
            _flights.Find<Airport>(flight => flight.Id == id).FirstOrDefault();

        public Airport Create(Airport flight)
        {
            _flights.InsertOne(flight);
            return flight;
        }

        public void Update(string id, Airport flightIn) =>
            _flights.ReplaceOne(flight => flight.Id == id, flightIn);

        public void Remove(Airport flightIn) =>
            _flights.DeleteOne(flight => flight.Id == flightIn.Id);

        public void Remove(string id) =>
            _flights.DeleteOne(flight => flight.Id == id);
    }
}
