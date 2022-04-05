using System.Collections.Generic;
using Models;
using MongoDB.Driver;
using ProjMongoDBApi.Utils;

namespace ProjMongoDBApi.Services
{
    public class AircraftService

    {
        private readonly IMongoCollection<Aircraft> _aircrafts;        

        public AircraftService(IProjMongoDBApiSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _aircrafts = database.GetCollection<Aircraft>(settings.AircraftCollectionName);
        }
        public Aircraft GetAircraftCode(string code) =>
           _aircrafts.Find<Aircraft>(aircraft => aircraft.Code == code).FirstOrDefault();
        public List<Aircraft> Get() =>
            _aircrafts.Find(passenger => true).ToList();

        public Aircraft Get(string id) =>
            _aircrafts.Find<Aircraft>(aircraft => aircraft.Id == id).FirstOrDefault();

        public Aircraft Create(Aircraft aircraft)
        {
            _aircrafts.InsertOne(aircraft);
            return aircraft;
        }

        public void Update(string id, Aircraft aircraftIn) =>
            _aircrafts.ReplaceOne(aircraft => aircraft.Id == id, aircraftIn);

        public void Remove(Aircraft aircraftIn) =>
            _aircrafts.DeleteOne(aircraft => aircraft.Id == aircraftIn.Id);

        public void Remove(string id) =>
            _aircrafts.DeleteOne(aircraft => aircraft.Id == id);
    }
}
