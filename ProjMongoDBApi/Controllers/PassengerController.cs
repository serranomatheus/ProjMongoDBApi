using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using ProjMongoDBApi.Services;
using ProjMongoDBPassenger.Services;

namespace ProjMongoDBApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
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

        private readonly PassengerService _passengerService;
        public PassengerController(PassengerService passengerService)
        {
            _passengerService = passengerService;
        }

        [HttpGet]
        [Authorize(Roles = "GetPassenger")]
        public ActionResult<List<Passenger>> Get() =>
            _passengerService.Get();
        
        [HttpGet("Search")]
        [Authorize(Roles = "GetPassengerCpf")]
        public ActionResult<Passenger> GetPassengerCpf(string cpf)
        {
            var passenger = _passengerService.GetCpf(cpf);
            if(passenger == null)
            {
                return NotFound();
            }
            return
                passenger;
        }

        [HttpGet("{id:length(24)}", Name = "GetPassenger")]
        [Authorize(Roles = "GetPassengerId")]
        public ActionResult<Passenger> Get(string id)
        {
            var passenger = _passengerService.Get(id);

            if (passenger == null)
            {
                return NotFound();
            }

            return passenger;
        }

        [HttpPost]
        [Authorize(Roles = "CreatePassenger")]
        public async Task<ActionResult<Passenger>> Create(Passenger passenger)
        {

            var addressApi = await Models.GetAddressApiPostalCodecs.GetAddress(passenger.Address.PostalCode);
            passenger.Address = new Address(addressApi.Street, addressApi.City, addressApi.FederativeUnit, addressApi.District, passenger.Address.Number,passenger.Address.Complement,addressApi.PostalCode) ;



            if (!CpfService.CheckCpfDB(passenger.Cpf, _passengerService))
                return null;
            var responseGetLogin = await GetLoginUser.GetLogin(passenger);

            if(responseGetLogin.Sucess == true)
            {
                _passengerService.Create(passenger);

                var passengerJson = JsonConvert.SerializeObject(passenger);
                PostLogApi.PostLog(new Log(passenger.LoginUser, null, passengerJson, "Create"));

                return Ok(passenger);
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
        [Authorize(Roles = "UpdatePassenger")]
        public IActionResult Update(string id, Passenger passengerIn)
        {
            var passenger = _passengerService.Get(id);

            if (passenger == null)
            {
                return NotFound();
            }

            var passengerJson = JsonConvert.SerializeObject(passenger);
            var passengerInJson = JsonConvert.SerializeObject(passengerIn);
            PostLogApi.PostLog(new Log(passengerIn.LoginUser, passengerJson, passengerInJson, "UpDate"));

            _passengerService.Update(id, passengerIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        [Authorize(Roles = "DeletePassenger")]
        public IActionResult Delete(string id)
        {
            var passenger = _passengerService.Get(id);

            if (passenger == null)
            {
                return NotFound();
            }

            var passengerJson = JsonConvert.SerializeObject(passenger);
            PostLogApi.PostLog(new Log(passenger.LoginUser, passengerJson, null, "Delete"));

            _passengerService.Remove(passenger.Id);

            return NoContent();
        }
        
      
    
    }
}
