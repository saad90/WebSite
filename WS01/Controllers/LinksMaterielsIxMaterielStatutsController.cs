using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WS01.Models;
using WS01.Services;

namespace WS01.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class LinksMaterielsIxMaterielStatutsController : Controller
    {
        private readonly WS01DBContext _context;
        EmailSender email = new EmailSender();

        public LinksMaterielsIxMaterielStatutsController(WS01DBContext context)
        {
            _context = context;
        }

        // GET: LinksMaterielsIxMaterielStatuts
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            var userNa = User.FindFirstValue(ClaimTypes.Name); // will give the user's userName
            var useradmin = _context.AspNetUserRoles.FirstOrDefault(c => c.RoleId == "1").UserId;
            if (userId == useradmin)
            {
                var wS01DBContext = _context.LinksMaterielsIxMaterielStatuts
                .Include(l => l.FkAspNetUsersNavigation)
                .Include(l => l.FkIxAntenneNavigation)
                .Include(l => l.FkMaterielsNavigation)
                .Include(l => l.FkMaterielsStatutsNavigation);
                return View(await wS01DBContext.ToListAsync());
            }
            else
            {
                var wS01DBContext = _context.LinksMaterielsIxMaterielStatuts
               .Include(l => l.FkAspNetUsersNavigation).Where(l => l.FkAspNetUsersNavigation.Id == userId)
               .Include(l => l.FkIxAntenneNavigation)
               .Include(l => l.FkMaterielsNavigation)
               .Include(l => l.FkMaterielsStatutsNavigation);
                return View(await wS01DBContext.ToListAsync());
            }
           
        }

        // GET: LinksMaterielsIxMaterielStatuts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var linksMaterielsIxMaterielStatuts = await _context.LinksMaterielsIxMaterielStatuts
                .Include(l => l.FkAspNetUsersNavigation)
                .Include(l => l.FkIxAntenneNavigation)
                .Include(l => l.FkMaterielsNavigation)
                .Include(l => l.FkMaterielsStatutsNavigation)
                .FirstOrDefaultAsync(m => m.Pk == id);
            if (linksMaterielsIxMaterielStatuts == null)
            {
                return NotFound();
            }

            return View(linksMaterielsIxMaterielStatuts);
        }

        // GET: LinksMaterielsIxMaterielStatuts/Create
        public IActionResult Create()
        {
            ViewData["FkAspNetUsers"] = new SelectList(_context.AspNetUsers, "Id", "UserName");
            ViewBag.idd = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.dat = DateTime.Now.ToString("dd MMMM yyyy");
            ViewData["FkIxAntenne"] = new SelectList(_context.IxAntenne, "PkAntenne", "Ville");
            ViewData["FkMateriels"] = new SelectList(_context.Materiels, "PkMateriels", "Identifiant");
            ViewData["FkMaterielsStatuts"] = new SelectList(_context.IxMaterielsStatuts, "PkIxMaterielsStatuts", "MaterielStatut");
            return View();
        }

        // POST: LinksMaterielsIxMaterielStatuts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Pk,FkMateriels,FkMaterielsStatuts,FkIxAntenne,FkAspNetUsers,DateDebut,DateFin,Commentaire")] LinksMaterielsIxMaterielStatuts linksMaterielsIxMaterielStatuts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(linksMaterielsIxMaterielStatuts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                                       .Where(y => y.Count > 0)
                                       .ToList();
            }
            ViewData["FkAspNetUsers"] = new SelectList(_context.AspNetUsers, "Id", "UserName");
            ViewData["FkIxAntenne"] = new SelectList(_context.IxAntenne, "PkAntenne", "Ville");
            ViewData["FkMateriels"] = new SelectList(_context.Materiels, "PkMateriels", "Identifiant");
            ViewData["FkMaterielsStatuts"] = new SelectList(_context.IxMaterielsStatuts, "PkIxMaterielsStatuts", "MaterielStatut");
            return View(linksMaterielsIxMaterielStatuts);
        }

        // GET: LinksMaterielsIxMaterielStatuts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var linksMaterielsIxMaterielStatuts = await _context.LinksMaterielsIxMaterielStatuts.FindAsync(id);
            if (linksMaterielsIxMaterielStatuts == null)
            {
                return NotFound();
            }
            //ViewData["FkMaterielsTypes"] = new SelectList(_context.IxMaterielsTypes, "PkIxMaterielsTypes", "PkIxMaterielsTypes", materiels.FkMaterielsTypes);
            //ViewData["MaterielType"] = new SelectList(_context.IxMaterielsTypes, "PkIxMaterielsTypes", "MaterielType", materiels.FkMaterielsTypes);
            ViewData["FkAspNetUsers"] = new SelectList(_context.AspNetUsers, "Id", "UserName");
            ViewData["FkIxAntenne"] = new SelectList(_context.IxAntenne, "PkAntenne", "Ville");
            ViewData["FkMateriels"] = new SelectList(_context.Materiels, "PkMateriels", "Identifiant");
            ViewData["FkMaterielsStatuts"] = new SelectList(_context.IxMaterielsStatuts, "PkIxMaterielsStatuts", "MaterielStatut");
            return View(linksMaterielsIxMaterielStatuts);
        }

        // POST: LinksMaterielsIxMaterielStatuts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Pk,FkMateriels,FkMaterielsStatuts,FkIxAntenne,FkAspNetUsers,DateDebut,DateFin,Commentaire")] LinksMaterielsIxMaterielStatuts linksMaterielsIxMaterielStatuts)
        {
            string mailsend = string.Empty;
            string sujet = string.Empty;
            string message = string.Empty;
            if (id != linksMaterielsIxMaterielStatuts.Pk)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if(linksMaterielsIxMaterielStatuts.FkMaterielsStatuts == 6)
                    {
                        linksMaterielsIxMaterielStatuts.DateFin = DateTime.Now.ToString("dd MMMM yyyy");
                    }
                    else
                    {
                        linksMaterielsIxMaterielStatuts.DateFin = null;
                    }
                    _context.Update(linksMaterielsIxMaterielStatuts);                  
                    mailsend = _context.AspNetUsers.First(c => c.Id == linksMaterielsIxMaterielStatuts.FkAspNetUsers).UserName;
                    message = _context.Materiels.First(c => c.PkMateriels == linksMaterielsIxMaterielStatuts.FkMateriels).Identifiant;
                    await email.SendEmailAsync(mailsend, "matériel modifié","Bonjour, \n Votre demande concernant le "+ message +" a été modifié. \n Cordialement,");
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LinksMaterielsIxMaterielStatutsExists(linksMaterielsIxMaterielStatuts.Pk))
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
            ViewData["FkAspNetUsers"] = new SelectList(_context.AspNetUsers, "Id", "Id", linksMaterielsIxMaterielStatuts.FkAspNetUsers);
            ViewData["FkIxAntenne"] = new SelectList(_context.IxAntenne, "PkAntenne", "PkAntenne", linksMaterielsIxMaterielStatuts.FkIxAntenne);
            ViewData["FkMateriels"] = new SelectList(_context.Materiels, "PkMateriels", "PkMateriels", linksMaterielsIxMaterielStatuts.FkMateriels);
            ViewData["FkMaterielsStatuts"] = new SelectList(_context.IxMaterielsStatuts, "PkIxMaterielsStatuts", "PkIxMaterielsStatuts", linksMaterielsIxMaterielStatuts.FkMaterielsStatuts);
            return View(linksMaterielsIxMaterielStatuts);
        }

        // GET: LinksMaterielsIxMaterielStatuts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var linksMaterielsIxMaterielStatuts = await _context.LinksMaterielsIxMaterielStatuts
                .Include(l => l.FkAspNetUsersNavigation)
                .Include(l => l.FkIxAntenneNavigation)
                .Include(l => l.FkMaterielsNavigation)
                .Include(l => l.FkMaterielsStatutsNavigation)
                .FirstOrDefaultAsync(m => m.Pk == id);
            if (linksMaterielsIxMaterielStatuts == null)
            {
                return NotFound();
            }

            return View(linksMaterielsIxMaterielStatuts);
        }

        // POST: LinksMaterielsIxMaterielStatuts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var linksMaterielsIxMaterielStatuts = await _context.LinksMaterielsIxMaterielStatuts.FindAsync(id);
            _context.LinksMaterielsIxMaterielStatuts.Remove(linksMaterielsIxMaterielStatuts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LinksMaterielsIxMaterielStatutsExists(int id)
        {
            return _context.LinksMaterielsIxMaterielStatuts.Any(e => e.Pk == id);
        }
    }
}
