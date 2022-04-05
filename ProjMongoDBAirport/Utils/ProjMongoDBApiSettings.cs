namespace ProjMongoDBApi.Utils
{
    public class ProjMongoDBApiSettings : IProjMongoDBApiSettings
    {
        public string AirportCollectionName { get; set ; }
        public string ConnectionString { get; set  ; }
        public string DatabaseName { get; set ; }
    }
}
