namespace ProjMongoDBUser.Utils
{
    public class ProjMongoDBApiSettings : IProjMongoDBApiSettings
    {
        public string UserCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
