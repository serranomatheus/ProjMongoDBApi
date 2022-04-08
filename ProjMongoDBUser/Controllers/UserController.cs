using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using ProjMongoDBUser.Services;

namespace ProjMongoDBUser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<List<User>> Get() =>
            _userService.Get();

        [HttpGet("Search")]
        public ActionResult<User> GetUserCpf(string cpf)
        {
            var user = _userService.GetCpf(cpf);
            if (user == null)
            {
                return NotFound();
            }
            return
                user;
        }


        [HttpGet("GetLogin")]
        public ActionResult<User> GetLogin(string loginUser)
        {
            var user = _userService.GetLogin(loginUser);
            if (user == null)
            {
                return NotFound();
            }
            return
                user;
        }

        [HttpGet("{id:length(24)}", Name = "GetUser")]
        public ActionResult<User> Get(string id)
        {
            var user = _userService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Create(User user)
        {

            var addressApi = await Models.GetAddressApiPostalCodecs.GetAddress(user.Address.PostalCode);
            user.Address = new Address(addressApi.Street, addressApi.City, addressApi.FederativeUnit, addressApi.District, user.Address.Number, user.Address.Complement, addressApi.PostalCode);

            if (!CpfService.CheckCpfDB(user.Cpf, _userService))
                return null;


            var responseGetLogin =  await CheckLoginUser.GetLogin(user);

            if (responseGetLogin.Sucess == true)
            {
                _userService.Create(user);
                
                var userJson = JsonConvert.SerializeObject(user);
               Services.PostLogApi.PostLog(new Log(user.LoginUser, null, userJson, "Create"));

                return CreatedAtRoute("GetUser", new { id = user.Id.ToString() }, user);
            }
            else
            {
                return GetResponse(responseGetLogin);
            }         

           
        }
        private ActionResult GetResponse(BaseResponse baseResponse)
        {
            if (baseResponse.Sucess == true)
            {
                return Ok(baseResponse.Result);
            }
            return BadRequest(baseResponse.Error);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, User userIn)
        {
            var user = _userService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            var userJson = JsonConvert.SerializeObject(user);
            var userInJson = JsonConvert.SerializeObject(userIn);
            Services.PostLogApi.PostLog(new Log(userIn.LoginUser, userJson, userInJson, "UpDate"));

            _userService.Update(id, userIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var user = _userService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            var userJson = JsonConvert.SerializeObject(user);
            Services.PostLogApi.PostLog(new Log(user.LoginUser, userJson, null, "Delete"));
            
            _userService.Remove(user.Id);

            return NoContent();
        }
    }
}
