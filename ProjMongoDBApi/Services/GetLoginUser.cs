using System.Net.Http;
using System.Threading.Tasks;
using Models;
using Newtonsoft.Json;

namespace ProjMongoDBPassenger.Services
{
    public class GetLoginUser
    {

        HttpClient ApiConnection = new HttpClient();
        public static async Task<BaseResponse> GetLogin(Passenger passenger)
        {
            var baseResponse = new BaseResponse();
            HttpClient ApiConnection = new HttpClient();

            HttpResponseMessage user = await ApiConnection.GetAsync("https://localhost:44320/api/User/GetLogin?loginUser=" + passenger.LoginUser);
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
                    baseResponse.ConnectionSucess(passenger);

                    return baseResponse;
                }
                else
                {
                    baseResponse.ConnectionError("User without permission");
                    return baseResponse;
                }

            }



        }
    }
}
