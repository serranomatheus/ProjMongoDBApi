using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] User model)
        {
            // Recupera o usuário
            var user = await GetUser.GetLogin(model.Login, model.PassWord);

            // Verifica se o usuário existe
            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            // Gera o Token
            var token = TokenService.GenerateToken(user);

            // Oculta a senha
            user.PassWord = "";

            // Retorna os dados
            return new
            {
                user = user,
                token = token
            };
        }

        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = "GetUser")]
        public ActionResult<List<User>> Get() =>
            _userService.Get();

        [HttpGet("Search")]
        [Authorize(Roles = "GetUserCpf")]
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
        [Authorize(Roles = "GetUserId")]
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
        [Authorize(Roles = "CreateUser")]
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
        [Authorize(Roles = "UpdateUser")]
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
        [Authorize(Roles = "DeleteUser")]
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
