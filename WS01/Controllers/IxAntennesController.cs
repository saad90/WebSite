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
    public class IxAntennesController : Controller
    {
        private readonly WS01DBContext _context;

        public IxAntennesController(WS01DBContext context)
        {
            _context = context;
        }

        // GET: IxAntennes
        public async Task<IActionResult> Index()
        {
            return View(await _context.IxAntenne.ToListAsync());
        }

        // GET: IxAntennes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ixAntenne = await _context.IxAntenne
                .FirstOrDefaultAsync(m => m.PkAntenne == id);
            if (ixAntenne == null)
            {
                return NotFound();
            }

            return View(ixAntenne);
        }

        // GET: IxAntennes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: IxAntennes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PkAntenne,Ville,CodePostal,Tel")] IxAntenne ixAntenne)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ixAntenne);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ixAntenne);
        }

        // GET: IxAntennes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ixAntenne = await _context.IxAntenne.FindAsync(id);
            if (ixAntenne == null)
            {
                return NotFound();
            }
            return View(ixAntenne);
        }

        // POST: IxAntennes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PkAntenne,Ville,CodePostal,Tel")] IxAntenne ixAntenne)
        {
            if (id != ixAntenne.PkAntenne)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ixAntenne);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IxAntenneExists(ixAntenne.PkAntenne))
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
            return View(ixAntenne);
        }

        // GET: IxAntennes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ixAntenne = await _context.IxAntenne
                .FirstOrDefaultAsync(m => m.PkAntenne == id);
            if (ixAntenne == null)
            {
                return NotFound();
            }

            return View(ixAntenne);
        }

        // POST: IxAntennes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ixAntenne = await _context.IxAntenne.FindAsync(id);
            _context.IxAntenne.Remove(ixAntenne);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IxAntenneExists(int id)
        {
            return _context.IxAntenne.Any(e => e.PkAntenne == id);
        }
    }
}
