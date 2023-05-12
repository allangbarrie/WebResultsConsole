using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Extensions;
using Microsoft.EntityFrameworkCore;
using WebAdminConsole.Models;
using WebAdminConsole.ViewModels;

namespace WebAdminConsole.Controllers
{
    public class LeaderBoardsController : Controller
    {
        private readonly AppIdentityDbContext _context;

        public LeaderBoardsController(AppIdentityDbContext context)
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

        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LeaderBoard == null)
            {
                return NotFound();
            }

            var leaderBoard = await _context.LeaderBoard
                .Include(l => l.TeamCategory)
                .Include(l => l.Team)
                .FirstOrDefaultAsync(m => m.LeaderBoardId == id);
            if (leaderBoard == null)
            {
                return NotFound();
            }

            return View(leaderBoard);
        }

        [Authorize]
        public IActionResult Create()
        {
            ViewData["StageId"] = new SelectList(_context.Set<Stage>(), "StageId", "Number");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StageId")] LeaderBoard leaderBoard)
        {
            var stageResults = await _context.Result
                .Where(u => u.StageId == leaderBoard.StageId)
                .ToListAsync();

            if (stageResults.Count == 0)
            {
                ViewData["NoResults"] = "Results not in yet.";
                ViewData["StageId"] = new SelectList(_context.Set<Stage>(), "StageId", "Number");
                return View();
            }

            var stage = await _context.Stage
                .Where(u => u.StageId == leaderBoard.StageId)
                .FirstOrDefaultAsync();

            var teams = await _context.Team
                .Include(u => u.TeamCategory)
                .ToListAsync();

            var modelList = new List<LeaderBoard>();

            foreach (var team in teams)
            {
                stageResults = await _context.Result
                    .Where(u => u.StageId <= leaderBoard.StageId)
                    .Where(u => u.BibNumber.Team.TeamId == team.TeamId)
                    .Include(u => u.BibNumber.Team)
                    .ToListAsync();

                TimeSpan total = stageResults
                    .Select(x => x.Time)
                    .Sum(x => x.Ticks)
                    .ToDateTime()
                    .TimeOfDay;

                var model = new LeaderBoard
                {
                    StageId = stage.StageId,
                    TeamId = team.TeamId,
                    Stage = stage,
                    Team = team,
                    Time = total,
                    TeamCategory = team.TeamCategory,
                    TeamCategoryId = team.TeamCategoryId,
                };

                modelList.Add(model);
            }

            int position = 1;

            modelList = modelList
                .OrderBy(x => x.Time)
                .ToList();

            var catPositions = new Dictionary<int, int>
            {
                { 1, 1 }, { 2, 1 }, { 3, 1 }, { 4, 1 },{ 5, 1 },{ 6, 1 }
            };

            foreach (var model in modelList)
            {
                model.Position = position++;
                model.Difference = model.Time - modelList
                    .Min(u => u.Time);

                model.CategoryDifference = model.Time - modelList
                    .Where(u => u.TeamCategoryId == model.TeamCategoryId)
                    .Min(u => u.Time);

                model.CategoryPosition = catPositions[model.TeamCategoryId];
                catPositions[model.TeamCategoryId]++;

                model.Stage = null;
                model.Team = null;
                model.TeamCategory = null;


                _context.Add(model);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
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

            var leaderBoard = await _context.LeaderBoard
                .Where(u => u.StageId == id)
                .Include(u => u.TeamCategory)
                .ToListAsync();

            if (leaderBoard.Count == 0)
            {
                ViewData["NoResults"] = "Results not in yet. Check back later.";
                return View();
            }

            var viewModel = new List<LeaderBoardViewModel>();

            foreach (var member in leaderBoard) 
            {
                var model = new LeaderBoardViewModel
                {
                    Position = member.Position,
                    Time = member.Time,
                    Difference = member.Difference,
                    Team = await _context.Team
                    .Where(u => u.TeamId == member.TeamId)
                    .Include (u => u.TeamCategory)
                    .FirstOrDefaultAsync(),
                    CatPosition = member.CategoryPosition,
                    CatDifference = member.CategoryDifference
            };

                viewModel.Add(model);
            }

            return View(viewModel);
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
            var results = await _context.LeaderBoard
                .Where(u => u.StageId == id)
                .ToListAsync();

            if (results != null)
            {
                foreach (var result in results) { _context.LeaderBoard.Remove(result); }
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool LeaderBoardExists(int id)
        {
          return (_context.LeaderBoard?.Any(e => e.LeaderBoardId == id)).GetValueOrDefault();
        }
    }
}
