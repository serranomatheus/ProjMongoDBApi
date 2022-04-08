namespace ProjMongoDBLog.Utils
{
    public interface IProjMongoDBApiSettings
    {
        string LogCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
