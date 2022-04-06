using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using ProjEntityApiAirport.Data;

namespace ProjEntityApiAirport.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportDataController : ControllerBase
    {
        private readonly ProjEntityApiAirportContext _context;

        public AirportDataController(ProjEntityApiAirportContext context)
        {
            _context = context;
        }

        // GET: api/AirportData
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AirportData>>> GetAirportData()
        {
            return await _context.AirportData.ToListAsync();
        }

        // GET: api/AirportData/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AirportData>> GetAirportData(int id)
        {
            var airportData = await _context.AirportData.FindAsync(id);

            if (airportData == null)
            {
                return NotFound();
            }

            return airportData;
        }

        // PUT: api/AirportData/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAirportData(int id, AirportData airportData)
        {
            if (id != airportData.Id)
            {
                return BadRequest();
            }

            _context.Entry(airportData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AirportDataExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/AirportData
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AirportData>> PostAirportData(AirportData airportData)
        {
            _context.AirportData.Add(airportData);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAirportData", new { id = airportData.Id }, airportData);
        }

        // DELETE: api/AirportData/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAirportData(int id)
        {
            var airportData = await _context.AirportData.FindAsync(id);
            if (airportData == null)
            {
                return NotFound();
            }

            _context.AirportData.Remove(airportData);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AirportDataExists(int id)
        {
            return _context.AirportData.Any(e => e.Id == id);
        }
    }
}
