using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data;
using WebAdminConsole.Models;
using WebAdminConsole.ViewModels;

namespace WebAdminConsole.Controllers
{
    [Authorize]
    public class TeamsController : Controller
    {
        private readonly AppIdentityDbContext _context;

        public TeamsController(AppIdentityDbContext context)
        {
            _context = context;

        }

        // GET: Teams
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Team.Include(t => t.Captain).Include(t => t.TeamCategory);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Teams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Team == null)
            {
                return NotFound();
            }

            var team = await _context.Team
                .Include(t => t.Captain)
                .Include(t => t.TeamCategory)
                .FirstOrDefaultAsync(m => m.TeamId == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // GET: Teams/Create
        public IActionResult Create()
        {
            ViewData["CaptainId"] = new SelectList(_context.Set<Captain>(), "CaptainId", "Name");
            ViewData["TeamCategoryId"] = new SelectList(_context.TeamCategory, "TeamCategoryId", "Name");
            return View();
        }

        // POST: Teams/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeamId,Name,CaptainId,TeamCategoryId")] Team team)
        {
            //if (ModelState.IsValid)
            //{
            _context.Add(team);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //}
            //ViewData["CaptainId"] = new SelectList(_context.Set<Captain>(), "CaptainId", "Name", team.CaptainId);
            //ViewData["TeamCategoryId"] = new SelectList(_context.TeamCategory, "TeamCategoryId", "Name", team.TeamCategoryId);
            //return View(team);
        }

        // GET: Teams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Team == null)
            {
                return NotFound();
            }

            var team = await _context.Team.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }
            ViewData["CaptainId"] = new SelectList(_context.Set<Captain>(), "CaptainId", "Name", team.CaptainId);
            ViewData["TeamCategoryId"] = new SelectList(_context.TeamCategory, "TeamCategoryId", "Name", team.TeamCategoryId);
            return View(team);
        }

        // POST: Teams/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeamId,Name,CaptainId,TeamCategoryId")] Team team)
        {
            if (id != team.TeamId)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {

                    _context.Update(team);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamExists(team.TeamId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            //}
            ViewData["CaptainId"] = new SelectList(_context.Set<Captain>(), "CaptainId", "Name", team.CaptainId);
            ViewData["TeamCategoryId"] = new SelectList(_context.TeamCategory, "TeamCategoryId", "Name", team.TeamCategoryId);
            return View(team);
        }

        // GET: Teams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Team == null)
            {
                return NotFound();
            }

            var team = await _context.Team
                .Include(t => t.Captain)
                .Include(t => t.TeamCategory)
                .FirstOrDefaultAsync(m => m.TeamId == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Team == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Team'  is null.");
            }
            var team = await _context.Team.FindAsync(id);
            if (team != null)
            {
                _context.Team.Remove(team);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult>  BulkAdd()
        {
            #region Teams
            List<BulkTeamCreateViewModel> newTeams = new List<BulkTeamCreateViewModel>();
            newTeams.Add(new BulkTeamCreateViewModel { StartNumber = 1, TeamName = "26.2 RRC 1", CaptainEmail = "contact@richardwkirk.co.uk" });
            newTeams.Add(new BulkTeamCreateViewModel { StartNumber = 15, TeamName = "26.2 RRC 2", CaptainEmail = "contact@richardwkirk.co.uk" });
            newTeams.Add(new BulkTeamCreateViewModel { StartNumber = 30, TeamName = "26.2 RRC 3", CaptainEmail = "contact@richardwkirk.co.uk" });
            newTeams.Add(new BulkTeamCreateViewModel { StartNumber = 45, TeamName = "BeaRCat Running Club 1", CaptainEmail = "jonlear@hotmail.co.uk" });
            newTeams.Add(new BulkTeamCreateViewModel { StartNumber = 60, TeamName = "BeaRCat Running Club 2", CaptainEmail = "jonlear@hotmail.co.uk" });
            newTeams.Add(new BulkTeamCreateViewModel { StartNumber = 75, TeamName = "Beckenham Running Club 1", CaptainEmail = "fletcherpaul90@gmail.com" });
            newTeams.Add(new BulkTeamCreateViewModel { StartNumber = 90, TeamName = "Beckenham Running Club 2", CaptainEmail = "fletcherpaul90@gmail.com" });
            newTeams.Add(new BulkTeamCreateViewModel { StartNumber = 105, TeamName = "Boston Consulting Group 1", CaptainEmail = "shenton.simon@bcg.com" });
            newTeams.Add(new BulkTeamCreateViewModel { StartNumber = 120, TeamName = "Boston Consulting Group 2", CaptainEmail = "shenton.simon@bcg.com" });
            newTeams.Add(new BulkTeamCreateViewModel { StartNumber = 135, TeamName = "British Airways AC 1", CaptainEmail = "christkelly@yahoo.com" });
            newTeams.Add(new BulkTeamCreateViewModel { StartNumber = 150, TeamName = "Burgess Hill Runners 1", CaptainEmail = "j.wadey@sky.com" });
            newTeams.Add(new BulkTeamCreateViewModel { StartNumber = 165, TeamName = "Burgess Hill Runners 2", CaptainEmail = "j.wadey@sky.com" });
            newTeams.Add(new BulkTeamCreateViewModel { StartNumber = 180, TeamName = "Burgess Hill Runners 3", CaptainEmail = "j.wadey@sky.com" });
            newTeams.Add(new BulkTeamCreateViewModel { StartNumber = 195, TeamName = "Clapham Chasers 1", CaptainEmail = "joespraggins@hotmail.co.uk" });
            newTeams.Add(new BulkTeamCreateViewModel { StartNumber = 210, TeamName = "Clapham Chasers 2", CaptainEmail = "joespraggins@hotmail.co.uk" });
            newTeams.Add(new BulkTeamCreateViewModel { StartNumber = 225, TeamName = "Clapham Chasers 3", CaptainEmail = "joespraggins@hotmail.co.uk" });
            newTeams.Add(new BulkTeamCreateViewModel { StartNumber = 240, TeamName = "Clapham Pioneers 1", CaptainEmail = "races@claphampioneers.co.uk" });
            newTeams.Add(new BulkTeamCreateViewModel { StartNumber = 255, TeamName = "Clapham Pioneers 2", CaptainEmail = "races@claphampioneers.co.uk" });
            newTeams.Add(new BulkTeamCreateViewModel { StartNumber = 270, TeamName = "Clapham Runners 1", CaptainEmail = "claphamrunners@gmail.com" });
            newTeams.Add(new BulkTeamCreateViewModel { StartNumber = 285, TeamName = "Dulwich Runners 1", CaptainEmail = "angenorris@googlemail.com" });
            newTeams.Add(new BulkTeamCreateViewModel { StartNumber = 300, TeamName = "Dulwich Runners 2", CaptainEmail = "angenorris@googlemail.com" });
            newTeams.Add(new BulkTeamCreateViewModel { StartNumber = 315, TeamName = "Ealing Eagles 1", CaptainEmail = "angeladuff81@gmail.com" });
            newTeams.Add(new BulkTeamCreateViewModel { StartNumber = 330, TeamName = "Ealing Eagles 2", CaptainEmail = "angeladuff81@gmail.com" });
            newTeams.Add(new BulkTeamCreateViewModel { StartNumber = 345, TeamName = "Ealing Eagles 3", CaptainEmail = "angeladuff81@gmail.com" });
            newTeams.Add(new BulkTeamCreateViewModel { StartNumber = 360, TeamName = "Elmbridge 1", CaptainEmail = "richnicholson@gmail.com" });
            newTeams.Add(new BulkTeamCreateViewModel { StartNumber = 375, TeamName = "Hampton Wick Wanderers 1", CaptainEmail = "simoncornish@mac.com" });
            newTeams.Add(new BulkTeamCreateViewModel { StartNumber = 390, TeamName = "Havering 90 Joggers 1", CaptainEmail = "bf4kg@icloud.com" });
            newTeams.Add(new BulkTeamCreateViewModel { StartNumber = 405, TeamName = "Hillingdon AC 1", CaptainEmail = "andywood3@hotmail.com" });
            newTeams.Add(new BulkTeamCreateViewModel { StartNumber = 420, TeamName = "London Front Runners 1", CaptainEmail = "julianandroman@me.com" });
            newTeams.Add(new BulkTeamCreateViewModel { StartNumber = 435, TeamName = "London Front Runners 2", CaptainEmail = "julianandroman@me.com" });
            newTeams.Add(new BulkTeamCreateViewModel { StartNumber = 450, TeamName = "London Front Runners 3", CaptainEmail = "julianandroman@me.com" });
            newTeams.Add(new BulkTeamCreateViewModel { StartNumber = 465, TeamName = "London Front Runners 4", CaptainEmail = "julianandroman@me.com" });
            newTeams.Add(new BulkTeamCreateViewModel { StartNumber = 480, TeamName = "Maidenhead AC 1", CaptainEmail = "nick_sille@hotmail.com" });
            newTeams.Add(new BulkTeamCreateViewModel { StartNumber = 495, TeamName = "Maidenhead AC 2", CaptainEmail = "nick_sille@hotmail.com" });
            newTeams.Add(new BulkTeamCreateViewModel { StartNumber = 510, TeamName = "Queens Park Harriers 1", CaptainEmail = "TimKLewin@gmail.com" });
            newTeams.Add(new BulkTeamCreateViewModel { StartNumber = 525, TeamName = "Ranelagh Harriers 1", CaptainEmail = "ranelaghgbr@gmail.com" });
            newTeams.Add(new BulkTeamCreateViewModel { StartNumber = 540, TeamName = "Ranelagh Harriers 2", CaptainEmail = "ranelaghgbr@gmail.com" });
            newTeams.Add(new BulkTeamCreateViewModel { StartNumber = 555, TeamName = "Ranelagh Harriers 3", CaptainEmail = "ranelaghgbr@gmail.com" });
            newTeams.Add(new BulkTeamCreateViewModel { StartNumber = 570, TeamName = "Serpentine 1", CaptainEmail = "hanssale4@yahoo.co.uk" });
            newTeams.Add(new BulkTeamCreateViewModel { StartNumber = 585, TeamName = "Serpentine 2", CaptainEmail = "hanssale4@yahoo.co.uk" });
            newTeams.Add(new BulkTeamCreateViewModel { StartNumber = 600, TeamName = "Serpentine 3", CaptainEmail = "hanssale4@yahoo.co.uk" });
            newTeams.Add(new BulkTeamCreateViewModel { StartNumber = 615, TeamName = "SHAEF Shifters 1", CaptainEmail = "ian.fullen@hotmail.com" });
            newTeams.Add(new BulkTeamCreateViewModel { StartNumber = 630, TeamName = "Stragglers 1", CaptainEmail = "simonwebb79@gmail.com" });
            newTeams.Add(new BulkTeamCreateViewModel { StartNumber = 645, TeamName = "Stragglers 2", CaptainEmail = "simonwebb79@gmail.com" });
            newTeams.Add(new BulkTeamCreateViewModel { StartNumber = 660, TeamName = "Sutton Striders 1", CaptainEmail = "brynreynolds1@hotmail.com" });
            newTeams.Add(new BulkTeamCreateViewModel { StartNumber = 675, TeamName = "Sutton Striders 2", CaptainEmail = "brynreynolds1@hotmail.com" });
            newTeams.Add(new BulkTeamCreateViewModel { StartNumber = 690, TeamName = "Team Bushy 1", CaptainEmail = "zoe.a.riding@gmail.com " });
            newTeams.Add(new BulkTeamCreateViewModel { StartNumber = 705, TeamName = "Thames Hare & Hounds 1", CaptainEmail = "m4ttjones@yahoo.co.uk" });
            newTeams.Add(new BulkTeamCreateViewModel { StartNumber = 720, TeamName = "Thames Hare & Hounds 2", CaptainEmail = "m4ttjones@yahoo.co.uk" });
            newTeams.Add(new BulkTeamCreateViewModel { StartNumber = 735, TeamName = "Waverley Harriers 1", CaptainEmail = "johnpetermac@hotmail.com" });
            newTeams.Add(new BulkTeamCreateViewModel { StartNumber = 750, TeamName = "Windmilers 1", CaptainEmail = "philreeves@outlook.com" });
            #endregion

            foreach (var team in newTeams)
            {
                //CheckCaptain
                var captainCheck = await _context.Captain.AnyAsync(x => x.Name == team.CaptainEmail);

                if (!captainCheck)
                {
                    var captain = new Captain
                    {
                        Name = team.CaptainEmail
                    };
                    _context.Add(captain);
                    await _context.SaveChangesAsync();
                }

                //CheckTeam
                if (!await _context.Team.AnyAsync(x => x.Name == team.TeamName))
                {
                    var captain = await _context.Captain.FirstAsync(x => x.Name == team.CaptainEmail);

                    var newTeam = new Team
                    {
                        Name = team.TeamName,
                        CaptainId = captain.CaptainId,
                        TeamCategoryId = 1
                    };
                    _context.Add(newTeam);
                    await _context.SaveChangesAsync();

                    //AddNumbers
                    var numberCheck = await _context.BibNumber.AnyAsync(x => x.Name == team.StartNumber.ToString());

                    for (int i = 0; i < 14; i++)
                    {
                        var num = team.StartNumber + i;
                        var newBibNumber = new BibNumber
                        {
                            Name = num.ToString(),
                            TeamId = newTeam.TeamId
                        };


                        _context.Add(newBibNumber);
                        await _context.SaveChangesAsync();
                    }



                }

            }
            

            return RedirectToAction(nameof(Index));
        }


        private bool TeamExists(int id)
        {
          return (_context.Team?.Any(e => e.TeamId == id)).GetValueOrDefault();
        }

        public async Task AddCaptain(string captainEmail)
        {
            var captain = new Captain
            {
                Name = captainEmail
            };
            _context.Add(captain);
            await _context.SaveChangesAsync();
        }

        public async Task AddTeam(BulkTeamCreateViewModel team)
        {
            var captain = await _context.Captain.FirstAsync(x => x.Name == team.CaptainEmail);

            var newTeam = new Team
            {
                Name = team.TeamName,
                CaptainId = captain.CaptainId,
                TeamCategoryId = 1
            };
            _context.Add(newTeam);
            await _context.SaveChangesAsync();
        }
    }
}
