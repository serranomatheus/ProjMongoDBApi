using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using ProjMongoDBAircraft.Services;
using ProjMongoDBApi.Services;

namespace ProjMongoDBAircraft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AircraftController : ControllerBase
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

        private readonly AircraftService _aircraftService;
        public AircraftController(AircraftService aircraftService)
        {
            _aircraftService = aircraftService;
        }

        [HttpGet]

        [Authorize(Roles = "GetAircraft")]
        public ActionResult<List<Aircraft>> Get() =>
            _aircraftService.Get();

        [HttpGet("Search")]
       
        [Authorize(Roles = "SearchAircraft")]
        public ActionResult<Aircraft> GetAircraftCode(string code)
        {
            var aircraft = _aircraftService.GetAircraftCode(code);
            if (aircraft == null)
            {
                return NotFound();
            }
            return
                aircraft;
        }

        [HttpGet("{id:length(24)}", Name = "GetAircraft")]
        
        [Authorize(Roles = "GetAircraftId")]
        public ActionResult<Aircraft> Get(string id)
        {
            var aircraft = _aircraftService.Get(id);

            if (aircraft == null)
            {
                return NotFound();
            }

            return aircraft;
        }

        [HttpPost]
      
        [Authorize(Roles = "CreateAircraft")]
        public async Task<ActionResult<Aircraft>> Create(Aircraft aircraft)
        {
            
            var responseGetLogin = await GetLoginUser.GetLogin(aircraft);

            if (responseGetLogin.Sucess == true)
            {
                _aircraftService.Create(aircraft);

                var aircraftJson = JsonConvert.SerializeObject(aircraft);
                Services.PostLogApi.PostLog(new Log(aircraft.LoginUser, null, aircraftJson, "Create"));

                return CreatedAtRoute("GetAircraft", new { id = aircraft.Id.ToString() }, aircraft);
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
      
        [Authorize(Roles = "UpdateAircraft")]
        public IActionResult Update(string id, Aircraft aircraftIn)
        {
            var aircraft = _aircraftService.Get(id);

            if (aircraft == null)
            {
                return NotFound();
            }

            var aircraftJson = JsonConvert.SerializeObject(aircraft);
            var aircraftInJson = JsonConvert.SerializeObject(aircraftIn);
            Services.PostLogApi.PostLog(new Log(aircraftIn.LoginUser, aircraftJson, aircraftInJson, "UpDate"));
            
            _aircraftService.Update(id, aircraftIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        
        [Authorize(Roles = "DeleteAircraft")]
        public IActionResult Delete(string id)
        {
            var aircraft = _aircraftService.Get(id);

            if (aircraft == null)
            {
                return NotFound();
            }

            var aircraftJson = JsonConvert.SerializeObject(aircraft);
            Services.PostLogApi.PostLog(new Log(aircraft.LoginUser, aircraftJson, null, "Delete"));

            _aircraftService.Remove(aircraft.Id);

            return NoContent();
        }
    }
}
