using Models;
using MongoDB.Driver;
using ProjRabbitMQLogApiToMongo.Utils;

namespace ProjRabbitMQLogApiToMongo.Services
{
    public class LogService
    {
        private readonly IMongoCollection<Log> _logs;

        public LogService(IProjMongoDBApiSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _logs = database.GetCollection<Log>(settings.LogCollectionName);
        }
        public Log Create(Log log)
        {
            _logs.InsertOne(log);
            return log;
        }

    }
}
