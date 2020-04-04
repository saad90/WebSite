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
    [Authorize(Roles = "Admin,Manager")]
    public class IxMaterielsTypesController : Controller
    {
        private readonly WS01DBContext _context;

        public IxMaterielsTypesController(WS01DBContext context)
        {
            _context = context;
        }

        // GET: IxMaterielsTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.IxMaterielsTypes.ToListAsync());
        }

        // GET: IxMaterielsTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ixMaterielsTypes = await _context.IxMaterielsTypes
                .FirstOrDefaultAsync(m => m.PkIxMaterielsTypes == id);
            if (ixMaterielsTypes == null)
            {
                return NotFound();
            }

            return View(ixMaterielsTypes);
        }

        // GET: IxMaterielsTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: IxMaterielsTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PkIxMaterielsTypes,MaterielType")] IxMaterielsTypes ixMaterielsTypes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ixMaterielsTypes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ixMaterielsTypes);
        }

        // GET: IxMaterielsTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ixMaterielsTypes = await _context.IxMaterielsTypes.FindAsync(id);
            if (ixMaterielsTypes == null)
            {
                return NotFound();
            }
            return View(ixMaterielsTypes);
        }

        // POST: IxMaterielsTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PkIxMaterielsTypes,MaterielType")] IxMaterielsTypes ixMaterielsTypes)
        {
            if (id != ixMaterielsTypes.PkIxMaterielsTypes)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ixMaterielsTypes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IxMaterielsTypesExists(ixMaterielsTypes.PkIxMaterielsTypes))
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
            return View(ixMaterielsTypes);
        }

        // GET: IxMaterielsTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ixMaterielsTypes = await _context.IxMaterielsTypes
                .FirstOrDefaultAsync(m => m.PkIxMaterielsTypes == id);
            if (ixMaterielsTypes == null)
            {
                return NotFound();
            }

            return View(ixMaterielsTypes);
        }

        // POST: IxMaterielsTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ixMaterielsTypes = await _context.IxMaterielsTypes.FindAsync(id);
            _context.IxMaterielsTypes.Remove(ixMaterielsTypes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IxMaterielsTypesExists(int id)
        {
            return _context.IxMaterielsTypes.Any(e => e.PkIxMaterielsTypes == id);
        }
    }
}
