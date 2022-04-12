using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace ProjRabbitMQLog.Services
{
    public class SenderSQLServerService
    {
        static readonly HttpClient client = new HttpClient();

        public static async Task Add(Log log)
        {
            try
            {
                if (client.BaseAddress == null) client.BaseAddress = new Uri("https://localhost:44371/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                await client.PostAsJsonAsync("api/log", log);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
    }
}
