using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using WebAdminConsole.Models;

namespace WebAdminConsole.Controllers
{
    [Authorize]
    public class MyTeamsController : Controller
    {
        private readonly AppIdentityDbContext _context;

        public MyTeamsController(AppIdentityDbContext context)
        {
            _context = context;
        }

        // GET: MyTeams
        public async Task<IActionResult> Index()
        {
            var myTeams = _context.Team
                .Include(r => r.TeamCategory)
                .Where(x => x.Captain.Name == User.Identity.Name);

            if (myTeams.Any())
            {
                return View(myTeams);
            }

            return NotFound();

        }

        // GET: Teams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Team == null)
            {
                return NotFound();
            }

            var team = _context.Team
                .Include(r => r.Captain)
                .Where(r => r.TeamId == id)
                .FirstOrDefault();

            if (team == null || team.Captain.Name != User.Identity.Name)
            {
                return NotFound();
            }
            ViewData["CaptainId"] = new SelectList(_context.Set<Captain>(), "CaptainId", "Name", team.CaptainId);
            ViewData["TeamCategoryId"] = new SelectList(_context.TeamCategory, "TeamCategoryId", "Name", team.TeamCategoryId);
            return View(team);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeamId,Name,CaptainId,TeamCategoryId")] Team team)
        {
            if (id != team.TeamId)
            {
                return NotFound();
            }

            try
            {

                _context.Update(team);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();

            }
            return RedirectToAction(nameof(Index));
            
        }

        // GET: MyTeams
        public async Task<IActionResult> ListRunners(int id)
        {
            var applicationDbContext = _context.Runner
                .Include(r => r.BibNumber)
                .Include(r => r.Category)
                .Include(r => r.Teams)
                .Where(r => r.TeamId == id);

            var teamName = _context.Team.Where(u => u.TeamId == id).FirstOrDefault();

            ViewData["TeamName"] = teamName.Name;

            ViewBag.TeamId = id;

            return View(await applicationDbContext.ToListAsync());

        }

        // GET: Runners/Create
        public IActionResult NewRunner(int id)
        {
            var bibNumber = _context.BibNumber
                .Where(u => u.TeamId == id);

            var teamName = _context.Team.Where(u => u.TeamId == id).FirstOrDefault();

            ViewData["TeamName"] = teamName.Name;

            ViewData["BibNumberId"] = new SelectList(bibNumber, "BibNumberId", "Name");
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name");
            ViewData["TeamId"] = id;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewRunner([Bind("RunnerId,First,Last,BibNumberId,TeamId,CategoryId")] Runner runner)
        {
            //Check Bibnumber
            if (!_context.Runner.Any(u => u.BibNumberId == runner.BibNumberId))
            {
                _context.Add(runner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ListRunners), new { id = runner.TeamId });
            }
            else
            {
                var existingRunner = _context.Runner.FirstOrDefault(u => u.BibNumberId.Equals(runner.BibNumberId));

                ViewData["Error"] = "Creation Failed: Number already assigned to another runner in your team.";

                var bibNumber = _context.BibNumber
                .Where(u => u.TeamId == runner.TeamId);

                ViewData["BibNumberId"] = new SelectList(bibNumber, "BibNumberId", "Name", runner.BibNumberId);
                ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name", runner.CategoryId);
                ViewData["TeamId"] = runner.TeamId;
                return View(runner);
            }
        }

        public async Task<IActionResult> EditRunner(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var runner = _context.Runner
                .Include(r => r.BibNumber)
                .Include(r => r.Teams)
                .Include(r => r.Teams.Captain)
                .Where(r => r.RunnerId == id)
                .FirstOrDefault();

            if (runner == null || runner.Teams.Captain.Name != User.Identity?.Name)
            {
                return NotFound();
            }

            ViewData["TeamName"] = runner.Teams.Name;

            var bibNumber = _context.BibNumber
            .Where(u => u.TeamId == runner.TeamId);

            ViewData["BibNumberId"] = new SelectList(bibNumber, "BibNumberId", "Name", runner.BibNumberId);
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name", runner.CategoryId);
            ViewData["TeamId"] = runner.TeamId;
            return View(runner);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRunner([Bind("RunnerId,First,Last,BibNumberId,TeamId,CategoryId")] Runner runner)
        {
            var thisRunner = _context.Runner
                .Where(u => u.RunnerId == runner.RunnerId)
                .FirstOrDefault();

            if (thisRunner != null)
            {
                if (thisRunner.BibNumberId == runner.BibNumberId)
                {
                    _context.Update(runner);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(ListRunners), new { id = runner.TeamId });
                }
            }

            if (!_context.Runner.Any(u => u.BibNumberId == runner.BibNumberId))
            {
                _context.Update(runner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ListRunners), new { id = runner.TeamId });
            }
            else
            {
                var existingRunner = _context.Runner.FirstOrDefault(u => u.BibNumberId.Equals(runner.BibNumberId));

                ViewData["Error"] = "Update Failed: Number already assigned to another runner in your team.";

                var bibNumber = _context.BibNumber
                .Where(u => u.TeamId == runner.TeamId);

                ViewData["BibNumberId"] = new SelectList(bibNumber, "BibNumberId", "Name", runner.BibNumberId);
                ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name", runner.CategoryId);
                ViewData["TeamId"] = runner.TeamId;
                return View(runner);
            }
        }

        // GET: Runners/Delete/5
        public async Task<IActionResult> DeleteRunner(int? id)
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

            ViewData["TeamName"] = runner.Teams.Name;
            ViewData["TeamId"] = runner.TeamId;

            return View(runner);
        }

        // POST: Runners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRunnerConfirmed(int id)
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
            return RedirectToAction(nameof(ListRunners), new { id = runner.TeamId });
        }
    }
}
