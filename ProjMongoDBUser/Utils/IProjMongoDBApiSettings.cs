namespace ProjMongoDBUser.Utils
{
    public interface IProjMongoDBApiSettings
    {
        string UserCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
