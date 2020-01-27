﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MaReB.Data;
using MaReB.Models;
using Microsoft.AspNetCore.Authorization;

namespace MaReB.Controllers
{
    [Authorize]
    public class CoordinatesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoordinatesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Coordinates
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Coordinate.Include(c => c.Comuna).Include(c => c.Provincia).Include(c => c.Region);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Coordinates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coordinate = await _context.Coordinate
                .Include(c => c.Comuna)
                .Include(c => c.Provincia)
                .Include(c => c.Region)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (coordinate == null)
            {
                return NotFound();
            }

            return View(coordinate);
        }

        // GET: Coordinates/Create
        public IActionResult Create()
        {
            ViewData["ComunaId"] = new SelectList(_context.Set<Comuna>(), "Id", "Id");
            ViewData["ProvinciaId"] = new SelectList(_context.Set<Provincia>(), "Id", "Id");
            ViewData["RegionId"] = new SelectList(_context.Set<Region>(), "Id", "Id");
            return View();
        }

        // POST: Coordinates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ComunaId,ProvinciaId,RegionId,CountryId,Latitude,Longitude,Vertex")] Coordinate coordinate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(coordinate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComunaId"] = new SelectList(_context.Set<Comuna>(), "Id", "Id", coordinate.ComunaId);
            ViewData["ProvinciaId"] = new SelectList(_context.Set<Provincia>(), "Id", "Id", coordinate.ProvinciaId);
            ViewData["RegionId"] = new SelectList(_context.Set<Region>(), "Id", "Id", coordinate.RegionId);
            return View(coordinate);
        }

        // GET: Coordinates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coordinate = await _context.Coordinate.SingleOrDefaultAsync(m => m.Id == id);
            if (coordinate == null)
            {
                return NotFound();
            }
            ViewData["ComunaId"] = new SelectList(_context.Set<Comuna>(), "Id", "Id", coordinate.ComunaId);
            ViewData["ProvinciaId"] = new SelectList(_context.Set<Provincia>(), "Id", "Id", coordinate.ProvinciaId);
            ViewData["RegionId"] = new SelectList(_context.Set<Region>(), "Id", "Id", coordinate.RegionId);
            return View(coordinate);
        }

        // POST: Coordinates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ComunaId,ProvinciaId,RegionId,CountryId,Latitude,Longitude,Vertex")] Coordinate coordinate)
        {
            if (id != coordinate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(coordinate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoordinateExists(coordinate.Id))
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
            ViewData["ComunaId"] = new SelectList(_context.Set<Comuna>(), "Id", "Id", coordinate.ComunaId);
            ViewData["ProvinciaId"] = new SelectList(_context.Set<Provincia>(), "Id", "Id", coordinate.ProvinciaId);
            ViewData["RegionId"] = new SelectList(_context.Set<Region>(), "Id", "Id", coordinate.RegionId);
            return View(coordinate);
        }

        // GET: Coordinates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coordinate = await _context.Coordinate
                .Include(c => c.Comuna)
                .Include(c => c.Provincia)
                .Include(c => c.Region)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (coordinate == null)
            {
                return NotFound();
            }

            return View(coordinate);
        }

        // POST: Coordinates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var coordinate = await _context.Coordinate.SingleOrDefaultAsync(m => m.Id == id);
            _context.Coordinate.Remove(coordinate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CoordinateExists(int id)
        {
            return _context.Coordinate.Any(e => e.Id == id);
        }
    }
}
