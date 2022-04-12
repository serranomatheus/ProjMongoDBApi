using System.Net.Http;
using System.Threading.Tasks;
using Models;
using Newtonsoft.Json;

namespace ProjMongoDBAirport.Services
{
    public class GetAiportData
    {
        HttpClient ApiConnection = new HttpClient();
        public static async Task<AirportData> GetAirport(string code)
        {

            HttpClient ApiConnection = new HttpClient();

            HttpResponseMessage airport = await ApiConnection.GetAsync("https://localhost:44366/api/AirportData/Code?code=" + code);
            string responseBody = await airport.Content.ReadAsStringAsync();
            var airportData = JsonConvert.DeserializeObject<AirportData>(responseBody);
            if (airportData.Code == null)
            {

                return null;
            }
            else
            {
                return airportData;
               

            }
        }
    }
}
