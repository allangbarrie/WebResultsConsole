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
    public class BibNumbersController : Controller
    {
        private readonly AppIdentityDbContext _context;

        public BibNumbersController(AppIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BibNumber.Include(b => b.Team);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BibNumber == null)
            {
                return NotFound();
            }

            var bibNumber = await _context.BibNumber
                .Include(b => b.Team)
                .FirstOrDefaultAsync(m => m.BibNumberId == id);
            if (bibNumber == null)
            {
                return NotFound();
            }

            return View(bibNumber);
        }

        public IActionResult Create()
        {
            ViewData["TeamId"] = new SelectList(_context.Set<Team>(), "TeamId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BibNumberId,Name,TeamId")] BibNumber bibNumber)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bibNumber);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TeamId"] = new SelectList(_context.Set<Team>(), "TeamId", "Name", bibNumber.TeamId);
            return View(bibNumber);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BibNumber == null)
            {
                return NotFound();
            }

            var bibNumber = await _context.BibNumber.FindAsync(id);
            if (bibNumber == null)
            {
                return NotFound();
            }
            ViewData["TeamId"] = new SelectList(_context.Set<Team>(), "TeamId", "Name", bibNumber.TeamId);
            return View(bibNumber);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BibNumberId,Name,TeamId")] BibNumber bibNumber)
        {
            if (id != bibNumber.BibNumberId)
            {
                return NotFound();
            }

            try
            {
                _context.Update(bibNumber);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BibNumberExists(bibNumber.BibNumberId))
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

        // GET: BibNumbers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BibNumber == null)
            {
                return NotFound();
            }

            var bibNumber = await _context.BibNumber
                .Include(b => b.Team)
                .FirstOrDefaultAsync(m => m.BibNumberId == id);
            if (bibNumber == null)
            {
                return NotFound();
            }

            return View(bibNumber);
        }

        // POST: BibNumbers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BibNumber == null)
            {
                return Problem("Entity set 'ApplicationDbContext.BibNumber'  is null.");
            }
            var bibNumber = await _context.BibNumber.FindAsync(id);
            if (bibNumber != null)
            {
                _context.BibNumber.Remove(bibNumber);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BibNumberExists(int id)
        {
          return (_context.BibNumber?.Any(e => e.BibNumberId == id)).GetValueOrDefault();
        }
    }
}
