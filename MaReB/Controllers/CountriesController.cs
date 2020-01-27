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
    public class CountriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CountriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Countries
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Country;
            return View(await applicationDbContext.ToListAsync());
        }

        [AllowAnonymous]
        // GET: Countries
        public IActionResult Map()
        {
            return View();
        }

        [AllowAnonymous]
        // GET: Regions
        public IActionResult Continents(bool p)
        {
            var data = from Continent c in _context.Continent
                       .Where(c => c.Id != 0 && c.Id != 6255152)
                       let a = p ? c.Countries.Sum(t => t.Exports
                    .Where(e => e.Species == Species.Almeja)
                    .Sum(e => e.Kg) ) : c.Countries.Sum(t => t.Exports
                    .Where(e => e.Species == Species.Almeja)
                    .Sum(e => (decimal)e.FOB))
                       let e = p ? c.Countries.Sum(t => t.Exports
                    .Where(e => e.Species == Species.Erizo)
                    .Sum(e => e.Kg)) : c.Countries.Sum(t => t.Exports
                   .Where(e => e.Species == Species.Erizo)
                   .Sum(e => (decimal)e.FOB))
                       let l = p ? c.Countries.Sum(t => t.Exports
                    .Where(e => e.Species == Species.Luga)
                    .Sum(e => e.Kg)) : c.Countries.Sum(t => t.Exports
                   .Where(e => e.Species == Species.Luga)
                   .Sum(e => (decimal)e.FOB))
                       select new
                       {
                           title = c.Name,
                           latitude = c.Latitude,
                           longitude = c.Longitude,
                           width = 15,
                           height = 15,
                           pieData = new object[] 
                           {
                               new { category = "Almeja", value = a },
                               new { category = "Erizo", value = e },
                               new { category = "Luga", value = l }
                           }
                       };
            return Json(data);
        }

        [AllowAnonymous]
        // GET: Regions
        public IActionResult Images(int id)
        {
            var sp = (Species)id;
            var images = from Country c in _context.Country
                        where c.Exports.Any(e => e.Species == sp)
                        select new
                        {
                            title = $"{c.Capital}, {c.Name}",
                            latitude = c.Latitude,
                            longitude = c.Longitude
                        };
            return Json(images);
        }

        [AllowAnonymous]
        // GET: Regions
        public IActionResult Lines(int id)
        {
            var sp = (Species)id;
            var lines = from Country c in _context.Country
                       where c.Exports.Any(e => e.Species == sp)
                       select new
                       {
                           multiGeoLine = new object[]
                           {
                               new object[]
                               {
                                   new
                                   {
                                       latitude = -33.4489,
                                       longitude = -70.6693
                                   },
                                   new
                                   {
                                       latitude = c.Latitude,
                                       longitude = c.Longitude
                                   }
                               }
                           }
                       };
            return Json(lines);
        }

        [AllowAnonymous]
        // GET: Regions
        public IActionResult Data(int id, bool p)
        {
            var sp = (Species)id;
            var data = from Country c in _context.Country
                       where c.Exports.Any(e => e.Species == sp)
                       let v = p ? c.Exports
                    .Where(e => e.Species == sp)
                    .Sum(e => e.Kg) : c.Exports
                    .Where(e => e.Species == sp)
                    .Sum(e => (decimal)e.FOB)
                    select new
                   {
                       id = c.ISO2,
                       value = (int)Math.Round((double)v / 1000, MidpointRounding.AwayFromZero)
                   };
            return Json(data);
        }

        // GET: Countries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Country
                .SingleOrDefaultAsync(m => m.Id == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // GET: Countries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ISO2,ISO3")] Country country)
        {
            if (ModelState.IsValid)
            {
                _context.Add(country);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        // GET: Countries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Country.SingleOrDefaultAsync(m => m.Id == id);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ISO2,ISO3")] Country country)
        {
            if (id != country.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(country);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountryExists(country.Id))
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
            return View(country);
        }

        // GET: Countries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Country
                .SingleOrDefaultAsync(m => m.Id == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var country = await _context.Country.SingleOrDefaultAsync(m => m.Id == id);
            _context.Country.Remove(country);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CountryExists(int id)
        {
            return _context.Country.Any(e => e.Id == id);
        }
    }
}
