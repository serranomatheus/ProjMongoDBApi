//using System.Net.Http;
//using System.Threading.Tasks;
//using Models;
//using Newtonsoft.Json;

//namespace ProjMongoDBBasePrice.Services
//{
//    public class GetAirport
//    {
//        public async Task<BaseResponse> GetAirport(BasePrice basePrice)
//        {
//            var baseResponse = new BaseResponse();
//            try
//            {
//                HttpClient ApiConnection = new HttpClient();
//                HttpResponseMessage airport = await ApiConnection.GetAsync("https://localhost:44340/api/Airports/GetCodeIata?codeIata=" + basePrice.Origin.CodeIata);

//                string responseBody = await airport.Content.ReadAsStringAsync();
//                var airportOrigin = JsonConvert.DeserializeObject<Airport>(responseBody);
//                if (airportOrigin.CodeIata == null)
//                {
//                    baseResponse.ConnectionError("Airport origin not found");
//                    return baseResponse;
//                }
//                else
//                {
//                    basePrice.Origin = airportOrigin;
                    
//                }
                    
               

//                airport = await ApiConnection.GetAsync("https://localhost:44340/api/Airports/GetCodeIata?codeIata=" + basePrice.Destination.CodeIata);

//                responseBody = await airport.Content.ReadAsStringAsync();
//                var airportDestination = JsonConvert.DeserializeObject<Airport>(responseBody);
//                if (airportDestination.CodeIata == null)
//                {
//                    baseResponse.ConnectionError("Airport origin not found");
//                    return baseResponse;
//                }
//                else
//                {
//                    basePrice.Destination = airportDestination;
//                    baseResponse.ConnectionSucess(basePrice);
//                    return baseResponse
//                }


//            }
//            catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
//            {
//                return Problem("Problem with connection  Airport Api");
//            }

//            _basePriceService.Create(basePrice);

//            return CreatedAtRoute("GetBasePrice", new { id = basePrice.Id.ToString() }, basePrice);
//        }
//    }
//}
