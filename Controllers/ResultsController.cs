using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using WebAdminConsole.Models;
using WebAdminConsole.ViewModels;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace WebService.Controllers
{
    [Authorize]
    public class ResultsController : Controller
    {
        private readonly AppIdentityDbContext _context;
        private IHostingEnvironment Environment;

        public ResultsController(AppIdentityDbContext context, IHostingEnvironment _environment)
        {
            _context = context;
            Environment = _environment;
        }

        public IActionResult Check()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Check(IFormFile postedFile)
        {
            if (postedFile != null)
            {
                var model = new List<UploadResultViewModel>();

                using (StreamReader csvReader = new StreamReader(postedFile.OpenReadStream()))
                {
                    while (!csvReader.EndOfStream)
                    {
                        var col = csvReader.ReadLine().Split(',');

                        int teamId = await _context.BibNumber
                            .Where(u => u.Name == col[1]).Select(u => u.TeamId)
                            .FirstOrDefaultAsync();

                        var modelRow = new UploadResultViewModel
                        {
                            Stage = await _context.Stage
                            .Where(u => u.Number == col[0])
                            .FirstOrDefaultAsync(),

                            BibNumber = await _context.BibNumber
                            .Where(u => u.Name == col[1])
                            .FirstOrDefaultAsync(),

                            Team = await _context.Team
                            .Where(u => u.TeamId == teamId)
                            .FirstOrDefaultAsync(),

                            Time = TimeSpan.Parse(col[2])
                        };

                        try
                        {
                            modelRow.Runner = await _context.Runner
                                .Where(u => u.BibNumberId == modelRow.BibNumber.BibNumberId)
                                .FirstOrDefaultAsync();
                        }
                        catch { }
                        if (modelRow.Runner is null)
                        {
                            Runner runner = new Runner
                            {
                                First = "Unknown",
                                Last = "Unknown"
                            };
                            modelRow.Runner = runner;
                        }

                        var cutOff = await _context.Stage
                        .Where(u => u.Number == col[0])
                        .Select(u => u.Cutoff)
                        .FirstOrDefaultAsync();

                        if (modelRow.Time > cutOff)
                        {
                            modelRow.Time = cutOff;
                        }

                        model.Add(modelRow);
                    }
                }

                //Find dupes
                List<UploadResultViewModel> SortedList = model.OrderBy(o => o.Time).ToList();
                var position = 1; 
                foreach (UploadResultViewModel row in SortedList) 
                {
                    row.Position = position;
                    position ++;

                    row.Count  = SortedList
                        .Where(u => u.Team.TeamId == row.Team.TeamId)
                        .ToList()
                        .Count();
                }

                //find missing teams
                var teamsList = await _context.Team.ToListAsync();

                foreach (UploadResultViewModel team in SortedList)
                {
                    try {
                        var itemToRemove = teamsList.Single(r => r.TeamId == team.Team.TeamId);
                        teamsList.Remove(itemToRemove);
                    }
                    catch{ }
                }

                if (teamsList.Count > 0)
                {
                    var errorMessage = "";
                    foreach (var team in teamsList)
                    {
                        errorMessage = string.Format("{0}, {1}", team.Name, errorMessage);
                    }

                    ViewData["Error"] = errorMessage; 
                }

                return View(SortedList);
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(IFormFile postedFile)
        {
            if (postedFile != null)
            {
                using (StreamReader csvReader = new StreamReader(postedFile.OpenReadStream()))
                {
                    while (!csvReader.EndOfStream)
                    {
                        var col = csvReader.ReadLine().Split(',');

                        var result = new Result
                        {
                            StageId = await _context.Stage
                            .Where(u => u.Number == col[0])
                            .Select(u => u.StageId)
                            .FirstOrDefaultAsync(),

                            BibNumberId = await _context.BibNumber
                            .Where(u => u.Name == col[1])
                            .Select (u => u.BibNumberId)
                            .FirstOrDefaultAsync(),

                            Time = TimeSpan.Parse(col[2])
                        };

                        var cutOff = await _context.Stage
                        .Where(u => u.Number == col[0])
                        .Select(u => u.Cutoff)
                        .FirstOrDefaultAsync();

                        if (result.Time > cutOff)
                        {
                            result.Time = cutOff;
                        }


                        _context.Add(result);
                        await _context.SaveChangesAsync();
                    }
                }

                return RedirectToAction(nameof(Index));

            }

            return View();
            
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Result.Include(r => r.BibNumber).Include(r => r.Stage);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Result == null)
            {
                return NotFound();
            }

            var result = await _context.Result
                .Include(r => r.BibNumber)
                .Include(r => r.Stage)
                .FirstOrDefaultAsync(m => m.ResultId == id);
            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        public IActionResult Create()
        {
            ViewData["BibNumberId"] = new SelectList(_context.BibNumber, "BibNumberId", "Name");
            ViewData["StageId"] = new SelectList(_context.Stage, "StageId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ResultId,StageId,Time,BibNumberId")] Result result)
        {
            if (ModelState.IsValid)
            {
                _context.Add(result);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BibNumberId"] = new SelectList(_context.BibNumber, "BibNumberId", "Name", result.BibNumberId);
            ViewData["StageId"] = new SelectList(_context.Stage, "StageId", "Name", result.StageId);
            return View(result);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Result == null)
            {
                return NotFound();
            }

            var result = await _context.Result.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            ViewData["BibNumberId"] = new SelectList(_context.BibNumber, "BibNumberId", "Name", result.BibNumberId);
            ViewData["StageId"] = new SelectList(_context.Stage, "StageId", "Name", result.StageId);
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ResultId,StageId,Time,BibNumberId")] Result result)
        {
            if (id != result.ResultId)
            {
                return NotFound();
            }

            try
            {
                _context.Update(result);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResultExists(result.ResultId))
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

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Result == null)
            {
                return NotFound();
            }

            var result = await _context.Result
                .Include(r => r.BibNumber)
                .Include(r => r.Stage)
                .FirstOrDefaultAsync(m => m.ResultId == id);
            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Result == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Result'  is null.");
            }
            var result = await _context.Result.FindAsync(id);
            if (result != null)
            {
                _context.Result.Remove(result);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResultExists(int id)
        {
          return (_context.Result?.Any(e => e.ResultId == id)).GetValueOrDefault();
        }
    }
}
