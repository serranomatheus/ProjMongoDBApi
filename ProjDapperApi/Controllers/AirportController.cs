using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using ProjDapperApiAirport.Services;

namespace ProjDapperApiAirport.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        private readonly AirportService _airportService = new AirportService();

        public AirportController(AirportService airportService)
        {
            _airportService = airportService;
        }

        [HttpGet]
        
        public ActionResult<List<AirportData>> Get() =>
            _airportService.GetAll();

        [HttpPost]
        
        public ActionResult<AirportData> Create(AirportData aiportData)
        {
            _airportService.Add(aiportData);
            return (aiportData);
        }

        [HttpDelete]
        
        public IActionResult Delete(string id)
        {
            var airportData = _airportService.Get(id);
            if(airportData == null)
            {
                return NotFound();
            }
            _airportService.Remove(id);
            return NoContent();
        }
        
        [HttpPut]
        
        public IActionResult Update(string id, AirportData airportDataIn)
        {
            var airportData = _airportService.Get(id);
            airportDataIn.Id = airportData.Id;
            if (airportData == null)
            {
                return NotFound();
            }
            _airportService.UpDate(airportDataIn);
            return NoContent();
        }
        
        [HttpGet("Code")]
        public ActionResult<AirportData> GetCode(string code) =>
        
            _airportService.GetCode(code);
        
        
   
        [HttpGet("Search")]
       
        public ActionResult<AirportData> Get(string id) =>
            _airportService.Get(id);

    }
}
