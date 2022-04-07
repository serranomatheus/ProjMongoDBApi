using System.Net.Http;
using System.Threading.Tasks;
using Models;
using Newtonsoft.Json;

namespace ProjMongoDBAirport.Services
{
    public class GetLoginUser
    {
        HttpClient ApiConnection = new HttpClient();
        public static async Task<BaseResponse> GetLogin(Airport airport)
        {
            var baseResponse = new BaseResponse();
            HttpClient ApiConnection = new HttpClient();

            HttpResponseMessage user = await ApiConnection.GetAsync("https://localhost:44320/api/User/GetLogin?loginUser=" + airport.LoginUser);
            string responseBody = await user.Content.ReadAsStringAsync();
            var userLogin = JsonConvert.DeserializeObject<User>(responseBody);
            if (userLogin.Login == null)
            {
                baseResponse.ConnectionError("User not found");
                return baseResponse;
            }
            else
            {
                if (userLogin.Role.Id == "1")
                {
                    baseResponse.ConnectionSucess(airport);

                    return baseResponse;
                }
                else
                {
                    baseResponse.ConnectionError("User without permission for create a Airport");
                    return baseResponse;
                }

            }
        }
    }
}
