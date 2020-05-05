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
    public class LinksInterventionsController : Controller
    {
        private readonly WS01DBContext _context;

        public LinksInterventionsController(WS01DBContext context)
        {
            _context = context;
        }

        // GET: LinksInterventions
        public async Task<IActionResult> Index()
        {
            var wS01DBContext = _context.LinksInterventions.Include(l => l.FkAspNetUsersNavigation).Include(l => l.FkInterventionsTypesNavigation).Include(l => l.FkIxAntenneNavigation).Include(l => l.FkMaterielsStatutsNavigation);
            return View(await wS01DBContext.ToListAsync());
        }

        // GET: LinksInterventions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var linksInterventions = await _context.LinksInterventions
                .Include(l => l.FkAspNetUsersNavigation)
                .Include(l => l.FkInterventionsTypesNavigation)
                .Include(l => l.FkIxAntenneNavigation)
                .Include(l => l.FkMaterielsStatutsNavigation)
                .FirstOrDefaultAsync(m => m.PkInterventions == id);
            if (linksInterventions == null)
            {
                return NotFound();
            }

            return View(linksInterventions);
        }

        // GET: LinksInterventions/Create
        public IActionResult Create()
        {
            ViewData["FkAspNetUsers"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            ViewData["FkInterventionsTypes"] = new SelectList(_context.IxInterventionsTypes, "PkIxInterventionsTypes", "InterventionType");
            ViewData["FkIxAntenne"] = new SelectList(_context.IxAntenne, "PkAntenne", "PkAntenne");
            ViewData["FkMaterielsStatuts"] = new SelectList(_context.IxMaterielsStatuts, "PkIxMaterielsStatuts", "PkIxMaterielsStatuts");
            return View();
        }

        // POST: LinksInterventions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PkInterventions,FkInterventionsTypes,FkAspNetUsers,FkIxAntenne,FkMaterielsStatuts,DateDebut,DateFin,Commentaire")] LinksInterventions linksInterventions)
        {
            if (ModelState.IsValid)
            {
                _context.Add(linksInterventions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkAspNetUsers"] = new SelectList(_context.AspNetUsers, "Id", "Id", linksInterventions.FkAspNetUsers);
            ViewData["FkInterventionsTypes"] = new SelectList(_context.IxInterventionsTypes, "PkIxInterventionsTypes", "InterventionType", linksInterventions.FkInterventionsTypes);
            ViewData["FkIxAntenne"] = new SelectList(_context.IxAntenne, "PkAntenne", "PkAntenne", linksInterventions.FkIxAntenne);
            ViewData["FkMaterielsStatuts"] = new SelectList(_context.IxMaterielsStatuts, "PkIxMaterielsStatuts", "PkIxMaterielsStatuts", linksInterventions.FkMaterielsStatuts);
            return View(linksInterventions);
        }

        // GET: LinksInterventions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var linksInterventions = await _context.LinksInterventions.FindAsync(id);
            if (linksInterventions == null)
            {
                return NotFound();
            }
            ViewData["FkAspNetUsers"] = new SelectList(_context.AspNetUsers, "Id", "Id", linksInterventions.FkAspNetUsers);
            ViewData["FkInterventionsTypes"] = new SelectList(_context.IxInterventionsTypes, "PkIxInterventionsTypes", "InterventionType", linksInterventions.FkInterventionsTypes);
            ViewData["FkIxAntenne"] = new SelectList(_context.IxAntenne, "PkAntenne", "PkAntenne", linksInterventions.FkIxAntenne);
            ViewData["FkMaterielsStatuts"] = new SelectList(_context.IxMaterielsStatuts, "PkIxMaterielsStatuts", "PkIxMaterielsStatuts", linksInterventions.FkMaterielsStatuts);
            return View(linksInterventions);
        }

        // POST: LinksInterventions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PkInterventions,FkInterventionsTypes,FkAspNetUsers,FkIxAntenne,FkMaterielsStatuts,DateDebut,DateFin,Commentaire")] LinksInterventions linksInterventions)
        {
            if (id != linksInterventions.PkInterventions)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(linksInterventions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LinksInterventionsExists(linksInterventions.PkInterventions))
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
            ViewData["FkAspNetUsers"] = new SelectList(_context.AspNetUsers, "Id", "Id", linksInterventions.FkAspNetUsers);
            ViewData["FkInterventionsTypes"] = new SelectList(_context.IxInterventionsTypes, "PkIxInterventionsTypes", "InterventionType", linksInterventions.FkInterventionsTypes);
            ViewData["FkIxAntenne"] = new SelectList(_context.IxAntenne, "PkAntenne", "PkAntenne", linksInterventions.FkIxAntenne);
            ViewData["FkMaterielsStatuts"] = new SelectList(_context.IxMaterielsStatuts, "PkIxMaterielsStatuts", "PkIxMaterielsStatuts", linksInterventions.FkMaterielsStatuts);
            return View(linksInterventions);
        }

        // GET: LinksInterventions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var linksInterventions = await _context.LinksInterventions
                .Include(l => l.FkAspNetUsersNavigation)
                .Include(l => l.FkInterventionsTypesNavigation)
                .Include(l => l.FkIxAntenneNavigation)
                .Include(l => l.FkMaterielsStatutsNavigation)
                .FirstOrDefaultAsync(m => m.PkInterventions == id);
            if (linksInterventions == null)
            {
                return NotFound();
            }

            return View(linksInterventions);
        }

        // POST: LinksInterventions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var linksInterventions = await _context.LinksInterventions.FindAsync(id);
            _context.LinksInterventions.Remove(linksInterventions);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LinksInterventionsExists(int id)
        {
            return _context.LinksInterventions.Any(e => e.PkInterventions == id);
        }
    }
}
