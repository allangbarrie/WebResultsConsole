using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAdminConsole.Models;
using WebAdminConsole.ViewModels;

namespace WebAdminConsole.Controllers
{
    [Authorize]
    public class RecordsController : Controller
    {
        private readonly AppIdentityDbContext _context;

        public RecordsController(AppIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Record.Include(x => x.Category).Include(x => x.Stage);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Check()
        {
            var modelList = new List<RecordCheckViewModel>();

            foreach (Stage stage in await _context.Stage.ToListAsync())
            {
                var menOverall = await _context.Record
                    .Where(u => u.StageId == stage.StageId)
                    .Where(u => u.CategoryId == 1).FirstOrDefaultAsync();
                var womenOverall = await _context.Record
                    .Where(u => u.StageId == stage.StageId)
                    .Where(u => u.CategoryId == 2)
                    .FirstOrDefaultAsync();

                foreach (Category category in await _context.Category.ToListAsync())
                {
                    var record = await _context.Record
                        .Where(u => u.StageId == stage.StageId)
                        .Where(u => u.CategoryId == category.CategoryId)
                        .FirstOrDefaultAsync();

                    foreach (Result result in await _context.Result.Where(u => u.StageId == stage.StageId).ToListAsync())
                    {
                        var runner = await _context.Runner
                            .Where(u => u.BibNumberId == result.BibNumberId)
                            .Include(u => u.Teams)
                            .FirstOrDefaultAsync();

                        if (runner.CategoryId == category.CategoryId && result.Time < record.Time)
                        {
                            var model = new RecordCheckViewModel
                            {
                                Stage = stage, Category = category, Overall = false, CategoryRecord = record.Time, NewTime = result.Time, Runner = runner
                            };

                            var isMale = runner.CategoryId == 1 || runner.CategoryId == 3 || runner.CategoryId == 5;

                            if (isMale && result.Time < menOverall.Time)
                            {
                                model.Overall = true;
                            }

                            if (!isMale && result.Time < womenOverall.Time)
                            {
                                model.Overall = true;
                            }


                            modelList.Add(model);
                        }

                    }
                }

            }

            return View(modelList);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Record == null)
            {
                return NotFound();
            }

            var @record = await _context.Record
                .Include(x => x.Category)
                .Include(x => x.Stage)
                .FirstOrDefaultAsync(m => m.RecordId == id);
            if (@record == null)
            {
                return NotFound();
            }

            return View(@record);
        }

        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name");
            ViewData["StageId"] = new SelectList(_context.Stage, "StageId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RecordId,Time,StageId,CategoryId")] Record @record)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@record);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name", @record.CategoryId);
            ViewData["StageId"] = new SelectList(_context.Stage, "StageId", "Name", @record.StageId);
            return View(@record);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Record == null)
            {
                return NotFound();
            }

            var @record = await _context.Record.FindAsync(id);
            if (@record == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name", @record.CategoryId);
            ViewData["StageId"] = new SelectList(_context.Stage, "StageId", "Name", @record.StageId);
            return View(@record);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RecordId,Time,StageId,CategoryId")] Record @record)
        {
            if (id != @record.RecordId)
            {
                return NotFound();
            }

            try
            {
                _context.Update(@record);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecordExists(@record.RecordId))
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
            if (id == null || _context.Record == null)
            {
                return NotFound();
            }

            var @record = await _context.Record
                .Include(x => x.Category)
                .Include(x => x.Stage)
                .FirstOrDefaultAsync(m => m.RecordId == id);
            if (@record == null)
            {
                return NotFound();
            }

            return View(@record);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Record == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Record'  is null.");
            }
            var @record = await _context.Record.FindAsync(id);
            if (@record != null)
            {
                _context.Record.Remove(@record);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecordExists(int id)
        {
          return (_context.Record?.Any(e => e.RecordId == id)).GetValueOrDefault();
        }
    }
}
