namespace ProjMongoDBUser.Utils
{
    public class ProjMongoDBApiSettings : IProjMongoDBApiSettings
    {
        public string UserCollectionName { get; set; } = "User";
        public string ConnectionString { get; set; } = "mongodb://localhost:27017";
        public string DatabaseName { get; set; } = "dbuser";
    }
}
