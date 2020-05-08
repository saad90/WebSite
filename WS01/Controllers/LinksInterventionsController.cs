using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WS01.Models;

namespace WS01.Controllers
{
    [Authorize(Roles = "Admin,User")]
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            var userNa = User.FindFirstValue(ClaimTypes.Name); // will give the user's userName
            var useradmin = _context.AspNetUserRoles.FirstOrDefault(c => c.RoleId == "1").UserId;
            if (userId == useradmin)
            {
                var wS01DBContext = _context.LinksInterventions.Include(l => l.FkAspNetUsersNavigation).Include(l => l.FkInterventionsTypesNavigation).Include(l => l.FkIxAntenneNavigation).Include(l => l.FkMaterielsStatutsNavigation);
                return View(await wS01DBContext.ToListAsync()); 
            }
            else
            {
                var wS01DBContext = _context.LinksInterventions.Include(l => l.FkAspNetUsersNavigation).Where(l => l.FkAspNetUsersNavigation.Id == userId).Include(l => l.FkInterventionsTypesNavigation).Include(l => l.FkIxAntenneNavigation).Include(l => l.FkMaterielsStatutsNavigation);
                return View(await wS01DBContext.ToListAsync());
            }
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
            ViewData["FkAspNetUsers"] = new SelectList(_context.AspNetUsers, "Id", "Email");
            ViewData["FkInterventionsTypes"] = new SelectList(_context.IxInterventionsTypes, "PkIxInterventionsTypes", "InterventionType");
            ViewData["FkIxAntenne"] = new SelectList(_context.IxAntenne, "PkAntenne", "Ville");
            ViewData["FkMaterielsStatuts"] = new SelectList(_context.IxMaterielsStatuts, "PkIxMaterielsStatuts", "MaterielStatut");
            ViewBag.dat = DateTime.Now.ToString("dd MMMM yyyy");
            ViewBag.idd = User.FindFirstValue(ClaimTypes.NameIdentifier);

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
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                                       .Where(y => y.Count > 0)
                                       .ToList();
            }
            ViewData["FkAspNetUsers"] = new SelectList(_context.AspNetUsers, "Id", "UserName", linksInterventions.FkAspNetUsers);
            ViewData["FkInterventionsTypes"] = new SelectList(_context.IxInterventionsTypes, "PkIxInterventionsTypes", "InterventionType", linksInterventions.FkInterventionsTypes);
            ViewData["FkIxAntenne"] = new SelectList(_context.IxAntenne, "PkAntenne", "Ville", linksInterventions.FkIxAntenne);
            ViewData["FkMaterielsStatuts"] = new SelectList(_context.IxMaterielsStatuts, "PkIxMaterielsStatuts", "MaterielStatut", linksInterventions.FkMaterielsStatuts);
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
            ViewData["FkAspNetUsers"] = new SelectList(_context.AspNetUsers, "Id", "UserName", linksInterventions.FkAspNetUsers);
            ViewData["FkInterventionsTypes"] = new SelectList(_context.IxInterventionsTypes, "PkIxInterventionsTypes", "InterventionType", linksInterventions.FkInterventionsTypes);
            ViewData["FkIxAntenne"] = new SelectList(_context.IxAntenne, "PkAntenne", "Ville", linksInterventions.FkIxAntenne);
            ViewData["FkMaterielsStatuts"] = new SelectList(_context.IxMaterielsStatuts, "PkIxMaterielsStatuts", "MaterielStatut", linksInterventions.FkMaterielsStatuts);
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
