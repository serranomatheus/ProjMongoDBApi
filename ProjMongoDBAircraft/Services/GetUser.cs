using System.Net.Http;
using System.Threading.Tasks;
using Models;
using Newtonsoft.Json;

namespace ProjMongoDBAircraft.Services
{
    public class GetUser
    {
        
        public static async Task<User> GetLogin(string login,string password)
        {
            
            HttpClient ApiConnection = new HttpClient();

            HttpResponseMessage user = await ApiConnection.GetAsync("https://localhost:44320/api/User/GetLogin?loginUser=" + login);
            string responseBody = await user.Content.ReadAsStringAsync();
            var userLogin = JsonConvert.DeserializeObject<User>(responseBody);
            if (userLogin.Login == null)
            {
                
                return null;
            }
            else
            {
                if (userLogin.PassWord.ToLower() == password.ToLower())
                {
                    

                    return userLogin;
                }
                else
                {
                    return null;
                }

            }
        }
    }
}
