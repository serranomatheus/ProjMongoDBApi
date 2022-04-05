using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using ProjMongoDBApi.Services;

namespace ProjMongoDBAirport.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportsController : ControllerBase
    {
        private readonly AirportService _airportService;
        public AirportsController(AirportService airportService)
        {
            _airportService = airportService;
        }

        [HttpGet]
        public ActionResult<List<Airport>> Get() =>
            _airportService.Get();


        [HttpGet("{id:length(24)}", Name = "GetAirport")]
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
        public ActionResult<Airport> Create(Airport airport)
        {
            _airportService.Create(airport);

            return CreatedAtRoute("GetAirport", new { id = airport.Id.ToString() }, airport);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Airport airportIn)
        {
            var airport = _airportService.Get(id);

            if (airport == null)
            {
                return NotFound();
            }

            _airportService.Update(id, airportIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var airport = _airportService.Get(id);

            if (airport == null)
            {
                return NotFound();
            }

            _airportService.Remove(airport.Id);

            return NoContent();
        }
    }
}
