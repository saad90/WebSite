﻿using System;
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

namespace WS01.Controllers
{
    [Authorize(Roles = "Admin,Manager,User")]
    public class LinksMaterielsIxMaterielStatutsController : Controller
    {
        private readonly WS01DBContext _context;

        public LinksMaterielsIxMaterielStatutsController(WS01DBContext context)
        {
            _context = context;
        }

        // GET: LinksMaterielsIxMaterielStatuts
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            var userName = User.FindFirstValue(ClaimTypes.Name); // will give the user's userName

            var wS01DBContext = _context.LinksMaterielsIxMaterielStatuts
                .Include(l => l.FkAspNetUsersNavigation).Where(n => n.FkAspNetUsersNavigation.Id == userId)
                .Include(l => l.FkIxAntenneNavigation)
                .Include(l => l.FkMaterielsNavigation)
                .Include(l => l.FkMaterielsStatutsNavigation);
            return View(await wS01DBContext.ToListAsync());
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
            ViewData["FkAspNetUsers"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            ViewData["FkIxAntenne"] = new SelectList(_context.IxAntenne, "PkAntenne", "PkAntenne");
            ViewData["FkMateriels"] = new SelectList(_context.Materiels, "PkMateriels", "PkMateriels");
            ViewData["FkMaterielsStatuts"] = new SelectList(_context.IxMaterielsStatuts, "PkIxMaterielsStatuts", "PkIxMaterielsStatuts");
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
            ViewData["FkAspNetUsers"] = new SelectList(_context.AspNetUsers, "Id", "Id", linksMaterielsIxMaterielStatuts.FkAspNetUsers);
            ViewData["FkIxAntenne"] = new SelectList(_context.IxAntenne, "PkAntenne", "PkAntenne", linksMaterielsIxMaterielStatuts.FkIxAntenne);
            ViewData["FkMateriels"] = new SelectList(_context.Materiels, "PkMateriels", "PkMateriels", linksMaterielsIxMaterielStatuts.FkMateriels);
            ViewData["FkMaterielsStatuts"] = new SelectList(_context.IxMaterielsStatuts, "PkIxMaterielsStatuts", "PkIxMaterielsStatuts", linksMaterielsIxMaterielStatuts.FkMaterielsStatuts);
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
            ViewData["FkAspNetUsers"] = new SelectList(_context.AspNetUsers, "Id", "Id", linksMaterielsIxMaterielStatuts.FkAspNetUsers);
            ViewData["FkIxAntenne"] = new SelectList(_context.IxAntenne, "PkAntenne", "PkAntenne", linksMaterielsIxMaterielStatuts.FkIxAntenne);
            ViewData["FkMateriels"] = new SelectList(_context.Materiels, "PkMateriels", "PkMateriels", linksMaterielsIxMaterielStatuts.FkMateriels);
            ViewData["FkMaterielsStatuts"] = new SelectList(_context.IxMaterielsStatuts, "PkIxMaterielsStatuts", "PkIxMaterielsStatuts", linksMaterielsIxMaterielStatuts.FkMaterielsStatuts);
            return View(linksMaterielsIxMaterielStatuts);
        }

        // POST: LinksMaterielsIxMaterielStatuts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Pk,FkMateriels,FkMaterielsStatuts,FkIxAntenne,FkAspNetUsers,DateDebut,DateFin,Commentaire")] LinksMaterielsIxMaterielStatuts linksMaterielsIxMaterielStatuts)
        {
            if (id != linksMaterielsIxMaterielStatuts.Pk)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(linksMaterielsIxMaterielStatuts);
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
