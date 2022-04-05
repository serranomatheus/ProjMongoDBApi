using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using ProjMongoDBApi.Services;

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
        public ActionResult<BasePrice> Create(BasePrice basePrice)
        {
            _basePriceService.Create(basePrice);

            return CreatedAtRoute("GetBasePrice", new { id = basePrice.Id.ToString() }, basePrice);
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
