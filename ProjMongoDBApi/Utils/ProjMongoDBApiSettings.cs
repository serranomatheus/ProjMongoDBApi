namespace ProjMongoDBApi.Utils
{
    public class ProjMongoDBApiSettings : IProjMongoDBApiSettings
    {
        public string PassengerCollectionName { get; set ; }
        public string ConnectionString { get; set  ; }
        public string DatabaseName { get; set ; }
    }
}
