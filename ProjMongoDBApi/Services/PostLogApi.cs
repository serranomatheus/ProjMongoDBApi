using System.Net.Http;
using System.Net.Http.Json;

namespace ProjMongoDBPassenger.Services
{
    public class PostLogApi
    {
        HttpClient ApiConnection = new HttpClient();
        public static void PostLog(Models.Log log)
        {
            HttpClient ApiConnection = new HttpClient();

            ApiConnection.PostAsJsonAsync("https://localhost:44395/api/Log", log);

        }
    }
}
