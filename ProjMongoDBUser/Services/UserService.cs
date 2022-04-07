using System.Collections.Generic;
using Models;
using MongoDB.Driver;
using ProjMongoDBUser.Utils;

namespace ProjMongoDBUser.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(IProjMongoDBApiSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<User>(settings.UserCollectionName);
        }
        public User GetCpf(string cpf) =>
            _users.Find<User>(user => user.Cpf == cpf).FirstOrDefault();
        public List<User> Get() =>
            _users.Find(user => true).ToList();

        public User Get(string id) =>
            _users.Find<User>(user => user.Id == id).FirstOrDefault();

        public User Create(User user)
        {
            _users.InsertOne(user);
            return user;
        }

        public void Update(string id, User userIn) =>
            _users.ReplaceOne(user => user.Id == id, userIn);

        public void Remove(User userIn) =>
            _users.DeleteOne(user => user.Id == userIn.Id);

        public void Remove(string id) =>
            _users.DeleteOne(user => user.Id == id);
    }
}
