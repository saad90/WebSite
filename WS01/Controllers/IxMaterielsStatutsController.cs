using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WS01.Models;

namespace WS01.Controllers
{
    [Authorize(Roles = "Admin")]
    public class IxMaterielsStatutsController : Controller
    {
        private readonly WS01DBContext _context;

        public IxMaterielsStatutsController(WS01DBContext context)
        {
            _context = context;
        }

        // GET: IxMaterielsStatuts
        public async Task<IActionResult> Index()
        {
            return View(await _context.IxMaterielsStatuts.ToListAsync());
            //var u = User.Identity;          
            //string query =
            //       (from us in _context.AspNetUsers
            //        join ro in _context.AspNetUserRoles on us.Id equals ro.UserId
            //        where ro.RoleId == "1" || ro.RoleId == "2"
            //        select us.UserName).FirstOrDefault().ToString();
            //if (query == u.Name)
            //{
            //    return View(await _context.IxMaterielsStatuts.ToListAsync());
            //}
            //else
            //{
            //    throw new Exception();
            //}

        }

        // GET: IxMaterielsStatuts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ixMaterielsStatuts = await _context.IxMaterielsStatuts
                .FirstOrDefaultAsync(m => m.PkIxMaterielsStatuts == id);
            if (ixMaterielsStatuts == null)
            {
                return NotFound();
            }

            return View(ixMaterielsStatuts);
        }

        // GET: IxMaterielsStatuts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: IxMaterielsStatuts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PkIxMaterielsStatuts,MaterielStatut")] IxMaterielsStatuts ixMaterielsStatuts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ixMaterielsStatuts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ixMaterielsStatuts);
        }

        // GET: IxMaterielsStatuts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ixMaterielsStatuts = await _context.IxMaterielsStatuts.FindAsync(id);
            if (ixMaterielsStatuts == null)
            {
                return NotFound();
            }
            return View(ixMaterielsStatuts);
        }

        // POST: IxMaterielsStatuts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PkIxMaterielsStatuts,MaterielStatut")] IxMaterielsStatuts ixMaterielsStatuts)
        {
            if (id != ixMaterielsStatuts.PkIxMaterielsStatuts)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ixMaterielsStatuts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IxMaterielsStatutsExists(ixMaterielsStatuts.PkIxMaterielsStatuts))
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
            return View(ixMaterielsStatuts);
        }

        // GET: IxMaterielsStatuts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ixMaterielsStatuts = await _context.IxMaterielsStatuts
                .FirstOrDefaultAsync(m => m.PkIxMaterielsStatuts == id);
            if (ixMaterielsStatuts == null)
            {
                return NotFound();
            }

            return View(ixMaterielsStatuts);
        }

        // POST: IxMaterielsStatuts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ixMaterielsStatuts = await _context.IxMaterielsStatuts.FindAsync(id);
            _context.IxMaterielsStatuts.Remove(ixMaterielsStatuts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IxMaterielsStatutsExists(int id)
        {
            return _context.IxMaterielsStatuts.Any(e => e.PkIxMaterielsStatuts == id);
        }
    }
}
