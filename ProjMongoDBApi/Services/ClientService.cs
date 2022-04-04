using System.Collections.Generic;
using MongoDB.Driver;
using ProjMongoDBApi.Model;
using ProjMongoDBApi.Utils;

namespace ProjMongoDBApi.Services
{
    public class ClientService
    {
        private readonly IMongoCollection<Person> _people;        

        public ClientService(IProjMongoDBApiSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _people = database.GetCollection<Person>(settings.ClientCollectionName);
        }

        public List<Person> Get() =>
            _people.Find(cliente => true).ToList();

        public Person Get(string id) =>
            _people.Find<Person>(person => person.Id == id).FirstOrDefault();

        public Person Create(Person person)
        {
            _people.InsertOne(person);
            return person;
        }

        public void Update(string id, Person personIn) =>
            _people.ReplaceOne(person => person.Id == id, personIn);

        public void Remove(Person personIn) =>
            _people.DeleteOne(person => person.Id == personIn.Id);

        public void Remove(string id) =>
            _people.DeleteOne(person => person.Id == id);
    }
}
