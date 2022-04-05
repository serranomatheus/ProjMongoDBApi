using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
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

        [HttpGet("Search")]
        public ActionResult<Flight> GetCodeIataAiport(string origin, string destination,DateTime arrivalTime)
        {
            var flight = _flightService.GetFlight(origin,destination,arrivalTime);
            if (flight == null)
            {
                return NotFound();
            }
            return
                flight;
        }

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
        public async Task<ActionResult<Flight>> Create(Flight flight)
        {
            try
            {
                HttpClient ApiConnection = new HttpClient();
                HttpResponseMessage airport = await ApiConnection.GetAsync("https://localhost:44340/api/Airports/GetCodeIata?codeIata=" + flight.Origin.CodeIata);
                
                string responseBody = await airport.Content.ReadAsStringAsync();                
                var airportOrigin = JsonConvert.DeserializeObject<Airport>(responseBody);
                if (airportOrigin.CodeIata == null)
                    return NotFound("Airport origin not found");
                flight.Origin = airportOrigin;

                airport = await ApiConnection.GetAsync("https://localhost:44340/api/Airports/GetCodeIata?codeIata=" + flight.Destination.CodeIata);
                
                responseBody = await airport.Content.ReadAsStringAsync();
                var airportDestination = JsonConvert.DeserializeObject<Airport>(responseBody);
                if (airportDestination.CodeIata == null)
                    return NotFound("Airport destination not found");
                flight.Destination = airportDestination;

                HttpResponseMessage aircraft = await ApiConnection.GetAsync("https://localhost:44340/api/Airports/GetCodeIata?codeIata=" + flight.Aircraft);
               
                string responseBody1 = await aircraft.Content.ReadAsStringAsync();
                var aircraftCode = JsonConvert.DeserializeObject<Aircraft>(responseBody1);
                if (aircraftCode.Code == null)
                    return NotFound("Aircraft nout found");
                flight.Aircraft = aircraftCode;


            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                throw;
            }

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
