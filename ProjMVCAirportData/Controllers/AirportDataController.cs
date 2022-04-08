using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjMVCAirportData.Data;
using ProjMVCAirportData.Models;

namespace ProjMVCAirportData.Controllers
{
    public class AirportDataController : Controller
    {
        private readonly ProjMVCAirportDataContext _context;

        public AirportDataController(ProjMVCAirportDataContext context)
        {
            _context = context;
        }

        // GET: AirportData
        public async Task<IActionResult> Index()
        {
            return View(await _context.AirportData.ToListAsync());
        }

        // GET: AirportData/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airportData = await _context.AirportData
                .FirstOrDefaultAsync(m => m.Id == id);
            if (airportData == null)
            {
                return NotFound();
            }

            return View(airportData);
        }

        // GET: AirportData/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AirportData/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,City,Country,Code,Continent")] AirportData airportData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(airportData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(airportData);
        }

        // GET: AirportData/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airportData = await _context.AirportData.FindAsync(id);
            if (airportData == null)
            {
                return NotFound();
            }
            return View(airportData);
        }

        // POST: AirportData/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,City,Country,Code,Continent")] AirportData airportData)
        {
            if (id != airportData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(airportData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AirportDataExists(airportData.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(airportData);
        }

        // GET: AirportData/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airportData = await _context.AirportData
                .FirstOrDefaultAsync(m => m.Id == id);
            if (airportData == null)
            {
                return NotFound();
            }

            return View(airportData);
        }

        // POST: AirportData/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var airportData = await _context.AirportData.FindAsync(id);
            _context.AirportData.Remove(airportData);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AirportDataExists(int id)
        {
            return _context.AirportData.Any(e => e.Id == id);
        }
    }
}
