namespace ProjMongoDBApi.Utils
{
    public class ProjMongoDBApiSettings : IProjMongoDBApiSettings
    {
        public string BasePriceCollectionName { get; set ; }
        public string ConnectionString { get; set  ; }
        public string DatabaseName { get; set ; }
    }
}
