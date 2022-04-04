namespace ProjMongoDBApi.Utils
{
    public interface IProjMongoDBApiSettings
    {
        string ClientCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
