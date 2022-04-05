namespace ProjMongoDBApi.Utils
{
    public interface IProjMongoDBApiSettings
    {
        string AirportCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
