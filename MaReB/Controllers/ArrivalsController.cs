using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MaReB.Data;
using MaReB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace MaReB.Controllers
{
    [Authorize]
    public class ArrivalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArrivalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Arrivals
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Arrival.Include(a => a.Comuna);
            return View(await applicationDbContext.ToListAsync());
        }

        [AllowAnonymous]
        public IActionResult Data()
        {
            var sp = (Species)0;
            var data = _context.Arrival
                .Include(a => a.Comuna)
                .Where(a => a.Species == sp)
                .GroupBy(a => a.Comuna)
                .Select(g =>
                new
                {
                    id = $"LL-{g.Key.Id}",
                    value = (int)Math.Round((double)g.Sum(a => a.Kg) / 1000, MidpointRounding.AwayFromZero)
                });
            return Json(data);
        }

        [AllowAnonymous]
        // GET: Arrivals
        public IActionResult Map()
        {
            return View();
        }

        [AllowAnonymous]
        // GET: Arrivals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var arrival = await _context.Arrival
                .Include(a => a.Comuna)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (arrival == null)
            {
                return NotFound();
            }

            return View(arrival);
        }

        // GET: Arrivals/Create
        public IActionResult Create()
        {
            ViewData["ComunaId"] = new SelectList(_context.Set<Comuna>(), "Id", "Id");
            return View();
        }

        // POST: Arrivals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ComunaId,Date,Species,Kg,Caleta")] Arrival arrival)
        {
            if (ModelState.IsValid)
            {
                _context.Add(arrival);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComunaId"] = new SelectList(_context.Set<Comuna>(), "Id", "Id", arrival.ComunaId);
            return View(arrival);
        }

        // GET: Arrivals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var arrival = await _context.Arrival.SingleOrDefaultAsync(m => m.Id == id);
            if (arrival == null)
            {
                return NotFound();
            }
            ViewData["ComunaId"] = new SelectList(_context.Set<Comuna>(), "Id", "Id", arrival.ComunaId);
            return View(arrival);
        }

        // POST: Arrivals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ComunaId,Date,Species,Kg,Caleta")] Arrival arrival)
        {
            if (id != arrival.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(arrival);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArrivalExists(arrival.Id))
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
            ViewData["ComunaId"] = new SelectList(_context.Set<Comuna>(), "Id", "Id", arrival.ComunaId);
            return View(arrival);
        }

        // GET: Arrivals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var arrival = await _context.Arrival
                .Include(a => a.Comuna)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (arrival == null)
            {
                return NotFound();
            }

            return View(arrival);
        }

        // POST: Arrivals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var arrival = await _context.Arrival.SingleOrDefaultAsync(m => m.Id == id);
            _context.Arrival.Remove(arrival);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArrivalExists(int id)
        {
            return _context.Arrival.Any(e => e.Id == id);
        }
    }
}
