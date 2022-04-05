namespace ProjMongoDBApi.Utils
{
    public interface IProjMongoDBApiSettings
    {
        string BasePriceCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
