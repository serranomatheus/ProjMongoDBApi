using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using ProjMongoDBApi.Services;
using ProjMongoDBBasePrice.Services;

namespace ProjMongoDBBasePrice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasePriceController : ControllerBase
    {
        private readonly BasePriceService _basePriceService;
        public BasePriceController(BasePriceService basePriceService)
        {
            _basePriceService = basePriceService;
        }

        [HttpGet]
        public ActionResult<List<BasePrice>> Get() =>
            _basePriceService.Get();
        [HttpGet("Search")]
        public ActionResult<BasePrice> GetBasePrice(string origin, string destination)
        {
            var basePrice = _basePriceService.GetBasePrice(origin, destination);
            if (basePrice == null)
            {
                return NotFound();
            }
            return
                basePrice;
        }

        [HttpGet("{id:length(24)}", Name = "GetBasePrice")]
        public ActionResult<BasePrice> Get(string id)
        {
            var basePrice = _basePriceService.Get(id);

            if (basePrice == null)
            {
                return NotFound();
            }

            return basePrice;
        }

        [HttpPost]
        public async Task<ActionResult<BasePrice>> Create(BasePrice basePrice)
        {
            try
            {
                HttpClient ApiConnection = new HttpClient();
                HttpResponseMessage airport = await ApiConnection.GetAsync("https://localhost:44340/api/Airports/GetCodeIata?codeIata=" + basePrice.Origin.CodeIata);
                
                string responseBody = await airport.Content.ReadAsStringAsync();
                var airportOrigin = JsonConvert.DeserializeObject<Airport>(responseBody);
                if (airportOrigin.CodeIata == null)
                    return NotFound("Airport origin not found");
                basePrice.Origin = airportOrigin;

                airport = await ApiConnection.GetAsync("https://localhost:44340/api/Airports/GetCodeIata?codeIata=" + basePrice.Destination.CodeIata);

                responseBody = await airport.Content.ReadAsStringAsync();
                var airportDestination = JsonConvert.DeserializeObject<Airport>(responseBody);
                if (airportDestination.CodeIata == null)
                    return NotFound("Airport destination not found");
                basePrice.Destination = airportDestination;

                

            }
            catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
            {
                return Problem("Problem with connection  Airport Api");
            }
            
            var responseGetLogin = await GetLoginUser.GetLogin(basePrice);

            if (responseGetLogin.Sucess == true)
            {
                _basePriceService.Create(basePrice);
                return CreatedAtRoute("GetBasePrice", new { id = basePrice.Id.ToString() }, basePrice);
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
        public IActionResult Update(string id, BasePrice basePriceIn)
        {
            var basePrice = _basePriceService.Get(id);

            if (basePrice == null)
            {
                return NotFound();
            }

            _basePriceService.Update(id, basePriceIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var basePrice = _basePriceService.Get(id);

            if (basePrice == null)
            {
                return NotFound();
            }

            _basePriceService.Remove(basePrice.Id);

            return NoContent();
        }
    }
}
