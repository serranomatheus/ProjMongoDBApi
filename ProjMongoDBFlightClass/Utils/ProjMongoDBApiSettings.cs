namespace ProjMongoDBApi.Utils
{
    public class ProjMongoDBApiSettings : IProjMongoDBApiSettings
    {
        public string FlightClassCollectionName { get; set ; }
        public string ConnectionString { get; set  ; }
        public string DatabaseName { get; set ; }
    }
}
