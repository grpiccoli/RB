using System;
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
    public class ComunasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComunasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Comunas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Communes.Include(c => c.Province);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Comunas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commune = await _context.Communes
                .Include(c => c.Province)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (commune == null)
            {
                return NotFound();
            }

            return View(commune);
        }

        // GET: Comunas/Create
        public IActionResult Create()
        {
            ViewData["ProvinceId"] = new SelectList(_context.Provinces, "Id", "Id");
            return View();
        }

        // POST: Comunas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProvinceId,Name,DE,CS")] Commune commune)
        {
            if (ModelState.IsValid)
            {
                _context.Add(commune);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProvinceId"] = new SelectList(_context.Provinces, "Id", "Id", commune.ProvinceId);
            return View(commune);
        }

        // GET: Comunas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commune = await _context.Communes.SingleOrDefaultAsync(m => m.Id == id);
            if (commune == null)
            {
                return NotFound();
            }
            ViewData["ProvinceId"] = new SelectList(_context.Provinces, "Id", "Id", commune.ProvinceId);
            return View(commune);
        }

        // POST: Comunas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProvinceId,Name,DE,CS")] Commune commune)
        {
            if (id != commune.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(commune);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComunaExists(commune.Id))
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
            ViewData["ProvinceId"] = new SelectList(_context.Provinces, "Id", "Id", commune.ProvinceId);
            return View(commune);
        }

        // GET: Comunas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commune = await _context.Communes
                .Include(c => c.Province)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (commune == null)
            {
                return NotFound();
            }

            return View(commune);
        }

        // POST: Comunas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var commune = await _context.Communes.SingleOrDefaultAsync(m => m.Id == id);
            _context.Communes.Remove(commune);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComunaExists(int id)
        {
            return _context.Communes.Any(e => e.Id == id);
        }
    }
}
