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
            var applicationDbContext = _context.Comuna.Include(c => c.Provincia);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Comunas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comuna = await _context.Comuna
                .Include(c => c.Provincia)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (comuna == null)
            {
                return NotFound();
            }

            return View(comuna);
        }

        // GET: Comunas/Create
        public IActionResult Create()
        {
            ViewData["ProvinciaId"] = new SelectList(_context.Provincia, "Id", "Id");
            return View();
        }

        // POST: Comunas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProvinciaId,Name,DE,CS")] Comuna comuna)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comuna);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProvinciaId"] = new SelectList(_context.Provincia, "Id", "Id", comuna.ProvinciaId);
            return View(comuna);
        }

        // GET: Comunas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comuna = await _context.Comuna.SingleOrDefaultAsync(m => m.Id == id);
            if (comuna == null)
            {
                return NotFound();
            }
            ViewData["ProvinciaId"] = new SelectList(_context.Provincia, "Id", "Id", comuna.ProvinciaId);
            return View(comuna);
        }

        // POST: Comunas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProvinciaId,Name,DE,CS")] Comuna comuna)
        {
            if (id != comuna.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comuna);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComunaExists(comuna.Id))
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
            ViewData["ProvinciaId"] = new SelectList(_context.Provincia, "Id", "Id", comuna.ProvinciaId);
            return View(comuna);
        }

        // GET: Comunas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comuna = await _context.Comuna
                .Include(c => c.Provincia)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (comuna == null)
            {
                return NotFound();
            }

            return View(comuna);
        }

        // POST: Comunas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comuna = await _context.Comuna.SingleOrDefaultAsync(m => m.Id == id);
            _context.Comuna.Remove(comuna);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComunaExists(int id)
        {
            return _context.Comuna.Any(e => e.Id == id);
        }
    }
}
