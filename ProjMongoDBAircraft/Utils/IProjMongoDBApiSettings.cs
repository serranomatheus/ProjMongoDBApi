namespace ProjMongoDBApi.Utils
{
    public interface IProjMongoDBApiSettings
    {
        string AircraftCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
