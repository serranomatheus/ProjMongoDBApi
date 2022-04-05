namespace ProjMongoDBApi.Utils
{
    public interface IProjMongoDBApiSettings
    {
        string TicketCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
