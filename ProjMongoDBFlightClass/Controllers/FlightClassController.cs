using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using ProjMongoDBApi.Services;

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
        public ActionResult<FlightClass> Create(FlightClass flightClass)
        {
            _flightClassService.Create(flightClass);

            return CreatedAtRoute("GetFlightClass", new { id = flightClass.Id.ToString() }, flightClass);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, FlightClass flightClassIn)
        {
            var flightClass = _flightClassService.Get(id);

            if (flightClass == null)
            {
                return NotFound();
            }

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

            _flightClassService.Remove(flightClass.Id);

            return NoContent();
        }
    }
}
