using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using ProjMongoDBApi.Services;
using ProjMongoDBFlight.Services;

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
        public ActionResult<Flight> GetFlight(string origin, string destination)
        {
            var flight = _flightService.GetFlight(origin,destination);
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

                HttpResponseMessage aircraft = await ApiConnection.GetAsync("https://localhost:44387/api/Aircraft/Search?code=" + flight.Aircraft);
               
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

            var responseGetLogin = await GetLoginUser.GetLogin(flight);

            if (responseGetLogin.Sucess == true)
            {
                _flightService.Create(flight);

                var flightJson = JsonConvert.SerializeObject(flight);
                Services.PostLogApi.PostLog(new Log(flight.LoginUser, null, flightJson, "Create"));

                return CreatedAtRoute("GetFlight", new { id = flight.Id.ToString() }, flight);
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
        public IActionResult Update(string id, Flight flightIn)
        {
            var flight = _flightService.Get(id);

            if (flight == null)
            {
                return NotFound();
            }

            var flightJson = JsonConvert.SerializeObject(flight);
            var flightInJson = JsonConvert.SerializeObject(flightIn);
            Services.PostLogApi.PostLog(new Log(flightIn.LoginUser, flightJson, flightInJson, "UpDate"));

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

            var flightjson = JsonConvert.SerializeObject(flight);
            Services.PostLogApi.PostLog(new Log(flight.LoginUser, flightjson, null, "Delete"));

            _flightService.Remove(flight.Id);

            return NoContent();
        }
    }
}
