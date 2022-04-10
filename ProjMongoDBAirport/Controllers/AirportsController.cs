using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using ProjMongoDBAirport.Services;
using ProjMongoDBApi.Services;

namespace ProjMongoDBAirport.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportsController : ControllerBase
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

        private readonly AirportService _airportService;
        public AirportsController(AirportService airportService)
        {
            _airportService = airportService;
        }

        [HttpGet]
        [Authorize(Roles = "GetAirport")]
        public ActionResult<List<Airport>> Get() =>
            _airportService.Get();
        [HttpGet("GetCodeIata")]
        [Authorize(Roles = "GetAirportCodeIata")]
        public ActionResult<Airport> GetCodeIataAiport(string codeIata)
        {
            var airport = _airportService.GetCodeIata(codeIata);
            if (airport == null)
            {
                return NotFound();
            }
            return
                airport;
        }


        [HttpGet("{id:length(24)}", Name = "GetAirport")]
        [Authorize(Roles = "GetAirportId")]
        public ActionResult<Airport> Get(string id)
        {
            var airport = _airportService.Get(id);

            if (airport == null)
            {
                return NotFound();
            }

            return airport;
        }

        [HttpPost]
        [Authorize(Roles = "CreateAirport")]
        public async Task<ActionResult<Airport>> Create(Airport airport)
        {
            var addressApi = await Models.GetAddressApiPostalCodecs.GetAddress(airport.Address.PostalCode);
            airport.Address = new Address(addressApi.Street, addressApi.City, addressApi.FederativeUnit, addressApi.District, airport.Address.Number, airport.Address.Complement,addressApi.PostalCode) ;



            var responseGetLogin = await GetLoginUser.GetLogin(airport);

            if (responseGetLogin.Sucess == true)
            {
                _airportService.Create(airport);

                var airportJson = JsonConvert.SerializeObject(airport);
                Services.PostLogApi.PostLog(new Log(airport.LoginUser, null, airportJson, "Create"));

                return CreatedAtRoute("GetAirport", new { id = airport.Id.ToString() }, airport);
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
        [Authorize(Roles = "UpdateAirport")]
        public IActionResult Update(string id, Airport airportIn)
        {
            var airport = _airportService.Get(id);

            if (airport == null)
            {
                return NotFound();
            }

            var airportJson = JsonConvert.SerializeObject(airport);
            var airportInJson = JsonConvert.SerializeObject(airportIn);
            Services.PostLogApi.PostLog(new Log(airportIn.LoginUser, airportJson, airportInJson, "UpDate"));

            _airportService.Update(id, airportIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        [Authorize(Roles = "DeleteAirport")]
        public IActionResult Delete(string id)
        {
            var airport = _airportService.Get(id);

            if (airport == null)
            {
                return NotFound();
            }

            var airportJson = JsonConvert.SerializeObject(airport);
            Services.PostLogApi.PostLog(new Log(airport.LoginUser, airportJson, null, "Delete"));

            _airportService.Remove(airport.Id);

            return NoContent();
        }
    }
}
