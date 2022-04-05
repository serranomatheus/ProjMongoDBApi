using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using ProjMongoDBApi.Services;

namespace ProjMongoDBFlight.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly FlightService _flightService;
        public FlightsController(FlightService flightService)
        {
            _flightService = flightService;
        }

        [HttpGet]
        public ActionResult<List<Flight>> Get() =>
            _flightService.Get();


        [HttpGet("{id:length(24)}", Name = "GetFlight")]
        public ActionResult<Flight> Get(string id)
        {
            var flight = _flightService.Get(id);

            if (flight == null)
            {
                return NotFound();
            }

            return flight;
        }

        [HttpPost]
        public ActionResult<Flight> Create(Flight flight)
        {
            _flightService.Create(flight);

            return CreatedAtRoute("GetFlight", new { id = flight.Id.ToString() }, flight);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Flight flightIn)
        {
            var flight = _flightService.Get(id);

            if (flight == null)
            {
                return NotFound();
            }

            _flightService.Update(id, flightIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var flight = _flightService.Get(id);

            if (flight == null)
            {
                return NotFound();
            }

            _flightService.Remove(flight.Id);

            return NoContent();
        }
    }
}
