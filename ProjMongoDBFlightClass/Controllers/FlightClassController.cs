using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using ProjMongoDBApi.Services;
using ProjMongoDBFlightClass.Services;

namespace ProjMongoDBFlightClass.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightClassController : ControllerBase
    {
        private readonly FlightClassService _flightClassService;
        public FlightClassController(FlightClassService flightClassService)
        {
            _flightClassService = flightClassService;
        }

        [HttpGet]
        public ActionResult<List<FlightClass>> Get() =>
            _flightClassService.Get();


        [HttpGet("{id:length(24)}", Name = "GetFlightClass")]
        public ActionResult<FlightClass> Get(string id)
        {
            var flightClass = _flightClassService.Get(id);

            if (flightClass == null)
            {
                return NotFound();
            }

            return flightClass;
        }

        [HttpPost]
        public async Task<ActionResult<FlightClass>> Create(FlightClass flightClass)
        {

            var responseGetLogin = await GetLoginUser.GetLogin(flightClass);

            if (responseGetLogin.Sucess == true)
            {
                _flightClassService.Create(flightClass);

                var flightClassJson = JsonConvert.SerializeObject(flightClass);
                Services.PostLogApi.PostLog(new Log(flightClass.LoginUser, null, flightClassJson, "Create"));

                return CreatedAtRoute("GetFlightClass", new { id = flightClass.Id.ToString() }, flightClass);
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
        public IActionResult Update(string id, FlightClass flightClassIn)
        {
            var flightClass = _flightClassService.Get(id);

            if (flightClass == null)
            {
                return NotFound();
            }

            var flightClassJson = JsonConvert.SerializeObject(flightClass);
            var flightClassInJson = JsonConvert.SerializeObject(flightClassIn);
            Services.PostLogApi.PostLog(new Log(flightClassIn.LoginUser, flightClassJson, flightClassInJson, "UpDate"));

            _flightClassService.Update(id, flightClassIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var flightClass = _flightClassService.Get(id);

            if (flightClass == null)
            {
                return NotFound();
            }
            var flightClassJson = JsonConvert.SerializeObject(flightClass);
            Services.PostLogApi.PostLog(new Log(flightClass.LoginUser, flightClassJson, null, "Delete"));

            _flightClassService.Remove(flightClass.Id);

            return NoContent();
        }
    }
}
