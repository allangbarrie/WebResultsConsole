using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAdminConsole.Models;
using WebAdminConsole.ViewModels;

namespace WebAdminConsole.Controllers
{
    
    public class StageResultsController : Controller
    {
        private readonly AppIdentityDbContext _context;

        public StageResultsController(AppIdentityDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return _context.Stage != null ?
            View(await _context.Stage.ToListAsync()) :
            Problem("Entity set 'ApplicationDbContext.Stage'  is null.");
        }

        [AllowAnonymous]
        public async Task<IActionResult> Result(int? id)
        {
            if (id == null || _context.Stage == null)
            {
                return NotFound();
            }

            ViewData["StageName"] = await _context.Stage
                .Where(u => u.StageId == id)
                .Select(u => u.Name)
                .FirstOrDefaultAsync();

            var results = await _context.Result
                .Where(m => m.StageId == id)
                .Include(m => m.BibNumber)
                .ToListAsync();

            if (results.Count == 0)
            {
                ViewData["NoResults"] = "Results not in yet. Check back later.";
                return View();
            }

            if (results == null)
            {
                return NotFound();
            }

            var winningTime = results
                .Min(u => u.Time);

            var viewModel = new List<StageResultsViewModel>();

            foreach (Result result in results)
            {
                var runner = await _context.Runner
                    .Where(u => u.BibNumberId == result.BibNumberId)
                    .Include(m => m.Category)
                    .Include(m => m.Teams)
                    .FirstOrDefaultAsync();

                var row = new StageResultsViewModel
                {
                    Time = result.Time,
                    Difference = result.Time - winningTime,
                    BibNumber = result.BibNumber,
                    Runner = runner,
                };

                viewModel.Add(row);
            }


            var position = 1;
            var catPositions = new Dictionary<int, int> { };

            foreach (var category in await _context.Category.ToListAsync())
            {
                catPositions.Add(category.CategoryId, 1);
            }

            foreach (StageResultsViewModel row in viewModel.OrderBy(o => o.Time).ToList())
            {
                row.Position = position;
                position++;

                row.CatDifference = row.Time - viewModel
                    .Where(u => u.Runner.Category.CategoryId == row.Runner.CategoryId)
                    .Min(u => u.Time);

                row.CatPosition = catPositions[row.Runner.CategoryId];
                catPositions[row.Runner.CategoryId]++;
            }

            return View(viewModel.OrderBy(u => u.Time));
        }

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Stage == null)
            {
                return NotFound();
            }

            var stage = await _context.Stage
                .FirstOrDefaultAsync(m => m.StageId == id);
            if (stage == null)
            {
                return NotFound();
            }

            ViewData["StageName"] = stage.Name;

            return View(stage);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Stage == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Stage'  is null.");
            }
            var results = await _context.Result
                .Where(u => u.StageId == id)
                .ToListAsync();

            if (results != null)
            {
                foreach(var result in results) { _context.Result.Remove(result); }
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
