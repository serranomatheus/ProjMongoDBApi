using System.Net.Http;
using System.Threading.Tasks;
using Models;
using Newtonsoft.Json;

namespace ProjMongoDBTicket.Services
{
    public class GetLoginUser
    {
        HttpClient ApiConnection = new HttpClient();
        public static async Task<BaseResponse> GetLogin(Ticket ticket)
        {
            var baseResponse = new BaseResponse();
            HttpClient ApiConnection = new HttpClient();

            HttpResponseMessage user = await ApiConnection.GetAsync("https://localhost:44320/api/User/GetLogin?loginUser=" + ticket.LoginUser);
            string responseBody = await user.Content.ReadAsStringAsync();
            var userLogin = JsonConvert.DeserializeObject<User>(responseBody);
            if (userLogin.Login == null)
            {
                baseResponse.ConnectionError("User not found");
                return baseResponse;
            }
            else
            {
                if (userLogin.Role.Id == "1" || userLogin.Role.Id == "2")
                {
                    baseResponse.ConnectionSucess(ticket);

                    return baseResponse;
                }
                else
                {
                    baseResponse.ConnectionError("User without permission for create a Ticket");
                    return baseResponse;
                }

            }
        }
    }
}
