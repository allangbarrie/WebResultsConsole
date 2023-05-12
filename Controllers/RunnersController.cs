using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAdminConsole.Models;

namespace WebService.Controllers
{
    [Authorize]
    public class RunnersController : Controller
    {
        private readonly AppIdentityDbContext _context;

        public RunnersController(AppIdentityDbContext context)
        {
            _context = context;
        }

        // GET: Runners
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Runner
                .Include(r => r.BibNumber)
                .Include(r => r.Category)
                .Include(r => r.Teams)
                .OrderBy(r => r.BibNumberId);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Runners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Runner == null)
            {
                return NotFound();
            }

            var runner = await _context.Runner
                .Include(r => r.BibNumber)
                .Include(r => r.Category)
                .Include(r => r.Teams)
                .FirstOrDefaultAsync(m => m.RunnerId == id);
            if (runner == null)
            {
                return NotFound();
            }

            return View(runner);
        }

        // GET: Runners/Create
        public IActionResult Create()
        {
            ViewData["BibNumberId"] = new SelectList(_context.BibNumber, "BibNumberId", "Name");
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name");
            ViewData["TeamId"] = new SelectList(_context.Set<Team>(), "TeamId", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RunnerId,First,Last,BibNumberId,TeamId,CategoryId")] Runner runner)
        {
            var teamId = await _context.BibNumber
                .Where(u => u.BibNumberId == runner.BibNumberId)
                .Select(u => u.TeamId)
                .FirstOrDefaultAsync();

            runner.TeamId = teamId;

            _context.Add(runner);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
            ViewData["BibNumberId"] = new SelectList(_context.BibNumber, "BibNumberId", "Name", runner.BibNumberId);
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name", runner.CategoryId);
            ViewData["TeamId"] = new SelectList(_context.Set<Team>(), "TeamId", "Name", runner.TeamId);
            return View(runner);
        }

        // GET: Runners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Runner == null)
            {
                return NotFound();
            }

            var runner = await _context.Runner.FindAsync(id);
            if (runner == null)
            {
                return NotFound();
            }
            ViewData["BibNumberId"] = new SelectList(_context.BibNumber, "BibNumberId", "Name", runner.BibNumberId);
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name", runner.CategoryId);
            ViewData["TeamId"] = new SelectList(_context.Set<Team>(), "TeamId", "Name", runner.TeamId);
            return View(runner);
        }

        // POST: Runners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RunnerId,First,Last,BibNumberId,TeamId,CategoryId")] Runner runner)
        {
            if (id != runner.RunnerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(runner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RunnerExists(runner.RunnerId))
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
            ViewData["BibNumberId"] = new SelectList(_context.BibNumber, "BibNumberId", "Name", runner.BibNumberId);
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name", runner.CategoryId);
            ViewData["TeamId"] = new SelectList(_context.Set<Team>(), "TeamId", "Name", runner.TeamId);
            return View(runner);
        }

        // GET: Runners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Runner == null)
            {
                return NotFound();
            }

            var runner = await _context.Runner
                .Include(r => r.BibNumber)
                .Include(r => r.Category)
                .Include(r => r.Teams)
                .FirstOrDefaultAsync(m => m.RunnerId == id);
            if (runner == null)
            {
                return NotFound();
            }

            return View(runner);
        }

        // POST: Runners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Runner == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Runner'  is null.");
            }
            var runner = await _context.Runner.FindAsync(id);
            if (runner != null)
            {
                _context.Runner.Remove(runner);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RunnerExists(int id)
        {
          return (_context.Runner?.Any(e => e.RunnerId == id)).GetValueOrDefault();
        }
    }
}
