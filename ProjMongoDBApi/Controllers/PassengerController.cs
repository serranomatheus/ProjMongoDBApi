using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

using ProjMongoDBApi.Services;
using ProjMongoDBPassenger.Services;

namespace ProjMongoDBApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {
        private readonly PassengerService _passengerService;
        public PassengerController(PassengerService passengerService)
        {
            _passengerService = passengerService;
        }

        [HttpGet]
        public ActionResult<List<Passenger>> Get() =>
            _passengerService.Get();
        
        [HttpGet("Search")]
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
        public IActionResult Update(string id, Passenger passengerIn)
        {
            var passenger = _passengerService.Get(id);

            if (passenger == null)
            {
                return NotFound();
            }

            _passengerService.Update(id, passengerIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var passenger = _passengerService.Get(id);

            if (passenger == null)
            {
                return NotFound();
            }

            _passengerService.Remove(passenger.Id);

            return NoContent();
        }
        
      
    
    }
}
