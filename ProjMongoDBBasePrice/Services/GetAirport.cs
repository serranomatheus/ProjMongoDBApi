using System;
using System.Net.Http;
using System.Threading.Tasks;
using Models;
using Newtonsoft.Json;

namespace ProjMongoDBBasePrice.Services
{
    public class GetAirport
    {
        static readonly HttpClient client = new HttpClient();
        public static async Task<Airport> GetAirportApi(string codeIata)
        {

            try
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:44340/api/Airports");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var airport = JsonConvert.DeserializeObject<Airport>(responseBody);

                return airport;

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                throw;
            }
        }
    }
}
