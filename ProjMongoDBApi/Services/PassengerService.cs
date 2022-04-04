using System.Collections.Generic;
using Models;
using MongoDB.Driver;
using ProjMongoDBApi.Utils;

namespace ProjMongoDBApi.Services
{
    public class PassengerService

    {
        private readonly IMongoCollection<Passenger> _passengers;        

        public PassengerService(IProjMongoDBApiSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _passengers = database.GetCollection<Passenger>(settings.ClientCollectionName);
        }

        public List<Passenger> Get() =>
            _passengers.Find(passenger => true).ToList();

        public Passenger Get(string id) =>
            _passengers.Find<Passenger>(passenger => passenger.Id == id).FirstOrDefault();

        public Passenger Create(Passenger passenger)
        {
            _passengers.InsertOne(passenger);
            return passenger;
        }

        public void Update(string id, Passenger passengerIn) =>
            _passengers.ReplaceOne(passenger => passenger.Id == id, passengerIn);

        public void Remove(Passenger passengerIn) =>
            _passengers.DeleteOne(passenger => passenger.Id == passengerIn.Id);

        public void Remove(string id) =>
            _passengers.DeleteOne(passenger => passenger.Id == id);
    }
}
