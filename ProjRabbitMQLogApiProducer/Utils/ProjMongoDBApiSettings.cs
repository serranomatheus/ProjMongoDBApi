namespace ProjRabbitMQLogApiToMongo.Utils
{
    public class ProjMongoDBApiSettings : IProjMongoDBApiSettings
    {
        public string LogCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

    }
}
