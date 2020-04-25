using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WS01.Models;

namespace WS01.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MaterielsController : Controller
    {
        private readonly WS01DBContext _context;

        public MaterielsController(WS01DBContext context)
        {
            _context = context;
        }

        // GET: Materiels
        public async Task<IActionResult> Index()
        {
            var wS01DBContext = _context.Materiels.Include(m => m.FkMaterielsTypesNavigation);
            return View(await wS01DBContext.ToListAsync());
        }

        // GET: Materiels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materiels = await _context.Materiels
                .Include(m => m.FkMaterielsTypesNavigation)
                .FirstOrDefaultAsync(m => m.PkMateriels == id);
            if (materiels == null)
            {
                return NotFound();
            }

            return View(materiels);
        }

        // GET: Materiels/Create
        public IActionResult Create()
        {
            ViewData["FkMaterielsTypes"] = new SelectList(_context.IxMaterielsTypes, "PkIxMaterielsTypes", "MaterielType");
            return View();
        }

        // POST: Materiels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PkMateriels,FkMaterielsTypes,Identifiant,DateAchat")] Materiels materiels)
        {
            if (ModelState.IsValid)
            {
                _context.Add(materiels);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkMaterielsTypes"] = new SelectList(_context.IxMaterielsTypes, "PkIxMaterielsTypes", "PkIxMaterielsTypes", materiels.FkMaterielsTypes);
            return View(materiels);
        }

        // GET: Materiels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materiels = await _context.Materiels.FindAsync(id);
            if (materiels == null)
            {
                return NotFound();
            }
            //ViewData["FkMaterielsTypes"] = new SelectList(_context.IxMaterielsTypes, "PkIxMaterielsTypes", "PkIxMaterielsTypes", materiels.FkMaterielsTypes);
            ViewData["MaterielType"] = new SelectList(_context.IxMaterielsTypes, "PkIxMaterielsTypes", "MaterielType", materiels.FkMaterielsTypes);

            return View(materiels);
        }

        // POST: Materiels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PkMateriels,FkMaterielsTypes,Identifiant,DateAchat")] Materiels materiels)
        {
            if (id != materiels.PkMateriels)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(materiels);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaterielsExists(materiels.PkMateriels))
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
            ViewData["FkMaterielsTypes"] = new SelectList(_context.IxMaterielsTypes, "PkIxMaterielsTypes", "FkMaterielsTypesNavigation.MaterielType", materiels.FkMaterielsTypes);
            return View(materiels);
        }

        // GET: Materiels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materiels = await _context.Materiels
                .Include(m => m.FkMaterielsTypesNavigation)
                .FirstOrDefaultAsync(m => m.PkMateriels == id);
            if (materiels == null)
            {
                return NotFound();
            }

            return View(materiels);
        }

        // POST: Materiels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var materiels = await _context.Materiels.FindAsync(id);
            _context.Materiels.Remove(materiels);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaterielsExists(int id)
        {
            return _context.Materiels.Any(e => e.PkMateriels == id);
        }
    }
}
