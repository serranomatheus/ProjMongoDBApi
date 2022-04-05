namespace ProjMongoDBApi.Utils
{
    public interface IProjMongoDBApiSettings
    {
        string PassengerCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
