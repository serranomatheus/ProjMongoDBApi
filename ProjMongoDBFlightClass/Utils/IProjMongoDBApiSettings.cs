namespace ProjMongoDBApi.Utils
{
    public interface IProjMongoDBApiSettings
    {
        string FlightClassCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
