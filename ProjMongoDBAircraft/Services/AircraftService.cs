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
            _aircrafts = database.GetCollection<Passenger>(settings.PassengerCollectionName);
        }

        public List<Passenger> Get() =>
            _aircrafts.Find(passenger => true).ToList();

        public Passenger Get(string id) =>
            _aircrafts.Find<Passenger>(passenger => passenger.Id == id).FirstOrDefault();

        public Passenger Create(Passenger passenger)
        {
            _aircrafts.InsertOne(passenger);
            return passenger;
        }

        public void Update(string id, Passenger passengerIn) =>
            _aircrafts.ReplaceOne(passenger => passenger.Id == id, passengerIn);

        public void Remove(Passenger passengerIn) =>
            _aircrafts.DeleteOne(passenger => passenger.Id == passengerIn.Id);

        public void Remove(string id) =>
            _aircrafts.DeleteOne(passenger => passenger.Id == id);
    }
}
