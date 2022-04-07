using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using ProjMongoDBAircraft.Services;
using ProjMongoDBApi.Services;

namespace ProjMongoDBAircraft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AircraftController : ControllerBase
    {
        private readonly AircraftService _aircraftService;
        public AircraftController(AircraftService aircraftService)
        {
            _aircraftService = aircraftService;
        }

        [HttpGet]
        public ActionResult<List<Aircraft>> Get() =>
            _aircraftService.Get();

        [HttpGet("Search")]
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
        public async Task<ActionResult<Aircraft>> Create(Aircraft aircraft)
        {
            
            var responseGetLogin = await GetLoginUser.GetLogin(aircraft);

            if (responseGetLogin.Sucess == true)
            {
                _aircraftService.Create(aircraft);
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
        public IActionResult Update(string id, Aircraft aircraftIn)
        {
            var aircraft = _aircraftService.Get(id);

            if (aircraft == null)
            {
                return NotFound();
            }

            _aircraftService.Update(id, aircraftIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var aircraft = _aircraftService.Get(id);

            if (aircraft == null)
            {
                return NotFound();
            }

            _aircraftService.Remove(aircraft.Id);

            return NoContent();
        }
    }
}
