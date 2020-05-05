using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WS01.Models;

namespace WS01.Controllers
{
    public class IxInterventionsTypesController : Controller
    {
        private readonly WS01DBContext _context;

        public IxInterventionsTypesController(WS01DBContext context)
        {
            _context = context;
        }

        // GET: IxInterventionsTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.IxInterventionsTypes.ToListAsync());
        }

        // GET: IxInterventionsTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ixInterventionsTypes = await _context.IxInterventionsTypes
                .FirstOrDefaultAsync(m => m.PkIxInterventionsTypes == id);
            if (ixInterventionsTypes == null)
            {
                return NotFound();
            }

            return View(ixInterventionsTypes);
        }

        // GET: IxInterventionsTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: IxInterventionsTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PkIxInterventionsTypes,InterventionType")] IxInterventionsTypes ixInterventionsTypes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ixInterventionsTypes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ixInterventionsTypes);
        }

        // GET: IxInterventionsTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ixInterventionsTypes = await _context.IxInterventionsTypes.FindAsync(id);
            if (ixInterventionsTypes == null)
            {
                return NotFound();
            }
            return View(ixInterventionsTypes);
        }

        // POST: IxInterventionsTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PkIxInterventionsTypes,InterventionType")] IxInterventionsTypes ixInterventionsTypes)
        {
            if (id != ixInterventionsTypes.PkIxInterventionsTypes)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ixInterventionsTypes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IxInterventionsTypesExists(ixInterventionsTypes.PkIxInterventionsTypes))
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
            return View(ixInterventionsTypes);
        }

        // GET: IxInterventionsTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ixInterventionsTypes = await _context.IxInterventionsTypes
                .FirstOrDefaultAsync(m => m.PkIxInterventionsTypes == id);
            if (ixInterventionsTypes == null)
            {
                return NotFound();
            }

            return View(ixInterventionsTypes);
        }

        // POST: IxInterventionsTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ixInterventionsTypes = await _context.IxInterventionsTypes.FindAsync(id);
            _context.IxInterventionsTypes.Remove(ixInterventionsTypes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IxInterventionsTypesExists(int id)
        {
            return _context.IxInterventionsTypes.Any(e => e.PkIxInterventionsTypes == id);
        }
    }
}
