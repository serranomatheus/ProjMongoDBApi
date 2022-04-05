using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
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
        public ActionResult<Aircraft> Create(Aircraft aircraft)
        {
            _aircraftService.Create(aircraft);

            return CreatedAtRoute("GetAircraft", new { id = aircraft.Id.ToString() }, aircraft);
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
