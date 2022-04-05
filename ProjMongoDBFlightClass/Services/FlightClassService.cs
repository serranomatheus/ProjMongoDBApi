using System.Collections.Generic;
using Models;
using MongoDB.Driver;
using ProjMongoDBApi.Utils;

namespace ProjMongoDBApi.Services
{
    public class FlightClassService

    {
        private readonly IMongoCollection<FlightClass> _flightClass;        

        public FlightClassService(IProjMongoDBApiSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _flightClass = database.GetCollection<FlightClass>(settings.FlightClassCollectionName);
        }

        public List<FlightClass> Get() =>
            _flightClass.Find(flightClass => true).ToList();

        public FlightClass Get(string id) =>
            _flightClass.Find<FlightClass>(flightClass => flightClass.Id == id).FirstOrDefault();

        public FlightClass Create(FlightClass flightClass)
        {
            _flightClass.InsertOne(flightClass);
            return flightClass;
        }

        public void Update(string id, FlightClass flightClassIn) =>
            _flightClass.ReplaceOne(flightClass => flightClass.Id == id, flightClassIn);

        public void Remove(FlightClass flightClassIn) =>
            _flightClass.DeleteOne(flightClass => flightClass.Id == flightClassIn.Id);

        public void Remove(string id) =>
            _flightClass.DeleteOne(flightClass => flightClass.Id == id);
    }
}
