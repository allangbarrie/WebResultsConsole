using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAdminConsole.Models;

namespace WebAdminConsole.Controllers
{
    [Authorize]
    public class TeamCategoriesController : Controller
    {
        private readonly AppIdentityDbContext _context;

        public TeamCategoriesController(AppIdentityDbContext context)
        {
            _context = context;
        }

        // GET: TeamCategories
        public async Task<IActionResult> Index()
        {
              return _context.TeamCategory != null ? 
                          View(await _context.TeamCategory.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.TeamCategory'  is null.");
        }

        // GET: TeamCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TeamCategory == null)
            {
                return NotFound();
            }

            var teamCategory = await _context.TeamCategory
                .FirstOrDefaultAsync(m => m.TeamCategoryId == id);
            if (teamCategory == null)
            {
                return NotFound();
            }

            return View(teamCategory);
        }

        // GET: TeamCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TeamCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeamCategoryId,Name")] TeamCategory teamCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teamCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(teamCategory);
        }

        // GET: TeamCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TeamCategory == null)
            {
                return NotFound();
            }

            var teamCategory = await _context.TeamCategory.FindAsync(id);
            if (teamCategory == null)
            {
                return NotFound();
            }
            return View(teamCategory);
        }

        // POST: TeamCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeamCategoryId,Name")] TeamCategory teamCategory)
        {
            if (id != teamCategory.TeamCategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teamCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamCategoryExists(teamCategory.TeamCategoryId))
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
            return View(teamCategory);
        }

        // GET: TeamCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TeamCategory == null)
            {
                return NotFound();
            }

            var teamCategory = await _context.TeamCategory
                .FirstOrDefaultAsync(m => m.TeamCategoryId == id);
            if (teamCategory == null)
            {
                return NotFound();
            }

            return View(teamCategory);
        }

        // POST: TeamCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TeamCategory == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TeamCategory'  is null.");
            }
            var teamCategory = await _context.TeamCategory.FindAsync(id);
            if (teamCategory != null)
            {
                _context.TeamCategory.Remove(teamCategory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamCategoryExists(int id)
        {
          return (_context.TeamCategory?.Any(e => e.TeamCategoryId == id)).GetValueOrDefault();
        }
    }
}
