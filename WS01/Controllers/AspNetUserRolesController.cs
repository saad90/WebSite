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
    public class AspNetUserRolesController : Controller
    {
        private readonly WS01DBContext _context;

        public AspNetUserRolesController(WS01DBContext context)
        {
            _context = context;
        }

        // GET: AspNetUserRoles
        public async Task<IActionResult> Index()
        {
            var wS01DBContext = _context.AspNetUserRoles.Include(a => a.Role).Include(a => a.User);
            return View(await wS01DBContext.ToListAsync());
        }

        // GET: AspNetUserRoles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aspNetUserRoles = await _context.AspNetUserRoles
                .Include(a => a.Role)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Pk == id);
            if (aspNetUserRoles == null)
            {
                return NotFound();
            }

            return View(aspNetUserRoles);
        }

        // GET: AspNetUserRoles/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "UserName");
            ViewData["RoleId"] = new SelectList(_context.AspNetRoles, "Id", "Name");
           
            return View();
        }

        // POST: AspNetUserRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Pk,UserId,RoleId")] AspNetUserRoles aspNetUserRoles)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aspNetUserRoles);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.AspNetRoles, "Id", "Id", aspNetUserRoles.RoleId);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", aspNetUserRoles.UserId);
            return View(aspNetUserRoles);
        }

        // GET: AspNetUserRoles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aspNetUserRoles = await _context.AspNetUserRoles.FindAsync(id);
            if (aspNetUserRoles == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "UserName", aspNetUserRoles.UserId);

            ViewData["RoleId"] = new SelectList(_context.AspNetRoles, "Id", "Name", aspNetUserRoles.RoleId);
            
            return View(aspNetUserRoles);
        }

        // POST: AspNetUserRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Pk,UserId,RoleId")] AspNetUserRoles aspNetUserRoles)
        {
            if (id != aspNetUserRoles.Pk)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aspNetUserRoles);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AspNetUserRolesExists(aspNetUserRoles.Pk))
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
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", "Id", aspNetUserRoles.UserId);
            ViewData["RoleId"] = new SelectList(_context.AspNetRoles, "Id", "Id", aspNetUserRoles.RoleId);
            
            return View(aspNetUserRoles);
        }

        // GET: AspNetUserRoles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aspNetUserRoles = await _context.AspNetUserRoles
                .Include(a => a.Role)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Pk == id);
            if (aspNetUserRoles == null)
            {
                return NotFound();
            }

            return View(aspNetUserRoles);
        }

        // POST: AspNetUserRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aspNetUserRoles = await _context.AspNetUserRoles.FindAsync(id);
            _context.AspNetUserRoles.Remove(aspNetUserRoles);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AspNetUserRolesExists(int id)
        {
            return _context.AspNetUserRoles.Any(e => e.Pk == id);
        }
    }
}
