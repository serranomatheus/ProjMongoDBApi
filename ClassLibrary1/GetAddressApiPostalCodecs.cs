using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Models
{
    public class GetAddressApiPostalCodecs
    {
        static readonly HttpClient client = new HttpClient();
        public static async Task<Address> GetAddress(string cep)
        {

            try
            {
                HttpResponseMessage address = await client.GetAsync("https://viacep.com.br/ws/" + cep + "/json/");
                address.EnsureSuccessStatusCode();
                string responseBody = await address.Content.ReadAsStringAsync();
                var addressObject = JsonConvert.DeserializeObject<Address>(responseBody);
                return addressObject;

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
