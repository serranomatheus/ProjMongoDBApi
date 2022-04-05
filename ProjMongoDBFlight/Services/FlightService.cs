using System.Collections.Generic;
using Models;
using MongoDB.Driver;
using ProjMongoDBApi.Utils;

namespace ProjMongoDBApi.Services
{
    public class FlightService

    {
        private readonly IMongoCollection<Flight> _flights;        

        public FlightService(IProjMongoDBApiSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _flights = database.GetCollection<Flight>(settings.FlightCollectionName);
        }

        public List<Flight> Get() =>
            _flights.Find(flight => true).ToList();

        public Flight Get(string id) =>
            _flights.Find<Flight>(flight => flight.Id == id).FirstOrDefault();

        public Flight Create(Flight flight)
        {
            _flights.InsertOne(flight);
            return flight;
        }

        public void Update(string id, Flight flightIn) =>
            _flights.ReplaceOne(flight => flight.Id == id, flightIn);

        public void Remove(Flight flightIn) =>
            _flights.DeleteOne(flight => flight.Id == flightIn.Id);

        public void Remove(string id) =>
            _flights.DeleteOne(flight => flight.Id == id);
    }
}
