using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAdminConsole.Models;

namespace WebAdminConsole.Controllers
{
    [Authorize]
    public class CaptainsController : Controller
    {
        private readonly AppIdentityDbContext _context;

        public CaptainsController(AppIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return _context.Captain != null ?
                        View(await _context.Captain.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Captain'  is null.");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Captain == null)
            {
                return NotFound();
            }

            var captain = await _context.Captain
                .FirstOrDefaultAsync(m => m.CaptainId == id);
            if (captain == null)
            {
                return NotFound();
            }

            return View(captain);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CaptainId,Name")] Captain captain)
        {
            if (ModelState.IsValid)
            {
                _context.Add(captain);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(captain);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Captain == null)
            {
                return NotFound();
            }

            var captain = await _context.Captain.FindAsync(id);
            if (captain == null)
            {
                return NotFound();
            }
            return View(captain);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CaptainId,Name")] Captain captain)
        {
            if (id != captain.CaptainId)
            {
                return NotFound();
            }

            try
            {
                _context.Update(captain);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CaptainExists(captain.CaptainId))
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

        // GET: Captains/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Captain == null)
            {
                return NotFound();
            }

            var captain = await _context.Captain
                .FirstOrDefaultAsync(m => m.CaptainId == id);
            if (captain == null)
            {
                return NotFound();
            }

            return View(captain);
        }

        // POST: Captains/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Captain == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Captain'  is null.");
            }
            var captain = await _context.Captain.FindAsync(id);
            if (captain != null)
            {
                _context.Captain.Remove(captain);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CaptainExists(int id)
        {
            return (_context.Captain?.Any(e => e.CaptainId == id)).GetValueOrDefault();
        }
    }
}
