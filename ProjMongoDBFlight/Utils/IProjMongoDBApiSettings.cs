namespace ProjMongoDBApi.Utils
{
    public interface IProjMongoDBApiSettings
    {
        string FlightCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
