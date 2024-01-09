using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BTRS.DATA;
using BTRS.Models;

namespace BTRS.Controllers
{
    public class BusController : Controller
    {
        private readonly SystemDbContext _context;

        public BusController(SystemDbContext context)
        {
            _context = context;
        }

        // GET: Buses
        public async Task<IActionResult> Index()
        {
            return _context.bus != null ?
                        View(await _context.bus.ToListAsync()) :
                        Problem("Entity set 'SystemDbContext.bus'  is null.");
        }

        // GET: Bus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.bus == null)
            {
                return NotFound();
            }

            var bus = await _context.bus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bus == null)
            {
                return NotFound();
            }

            return View(bus);
        }

        // GET: Bus/Create
        public IActionResult Create()
        {
            ViewBag.Trips = _context.bus.ToList();
            return View();
        }

        // POST: Bus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection form)
        {
            int tripsId = int.Parse(form["tripsId"]);
            string CaptainName = form["CaptainName"].ToString();
            int NumOfSeats = int.Parse(form["NumOfSeats"]);

            Bus bus = new Bus();
            bus.CaptainName = CaptainName;
            bus.NumOfSeats = NumOfSeats;

       

            _context.bus.Add(bus);
            _context.SaveChanges();

            return RedirectToAction("Index");




        }

        // GET: Bus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.bus == null)
            {
                return NotFound();
            }

            var bus = await _context.bus.FindAsync(id);
            if (bus == null)
            {
                return NotFound();
            }
            return View(bus);
        }

        // POST: Bus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CaptainName,NumOfSeats")] Bus bus)
        {
            if (id != bus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BusExists(bus.Id))
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
            return View(bus);
        }

        // GET: Bus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.bus == null)
            {
                return NotFound();
            }

            var bus = await _context.bus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bus == null)
            {
                return NotFound();
            }

            return View(bus);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.bus == null)
            {
                return Problem("Entity set 'SystemDbContext.bus'  is null.");
            }
            var bus = await _context.bus.FindAsync(id);
            if (bus != null)
            {
                _context.bus.Remove(bus);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BusExists(int id)
        {
            return (_context.bus?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
