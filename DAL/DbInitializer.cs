using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebAdminConsole.Models;
using WebAdminConsole.ViewModels;

namespace WebAdminConsole.DAL
{
    internal class DbInitializer
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<AppUser> userManager;

        public DbInitializer(RoleManager<IdentityRole> roleMgr, UserManager<AppUser> userMrg)
        {
            roleManager = roleMgr;
            userManager = userMrg;
        }

        internal static async Task Initialize(AppIdentityDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context, nameof(context));
            context.Database.EnsureCreated();

            //Stages
            if (context.Stage.Any())
            {
                return;   // DB has been seeded
            }
            var stages = new Stage[]
            {
                new Stage { Number = "Stage 1", Name = "Hampton Court", Cutoff = TimeSpan.Parse("02:18") },
                new Stage { Number = "Stage 2", Name = "Staines", Cutoff = TimeSpan.Parse("01:41") },
                new Stage { Number = "Stage 3", Name = "Boveney Church", Cutoff = TimeSpan.Parse("02:01") },
                new Stage { Number = "Stage 4", Name = "Little Marlow", Cutoff = TimeSpan.Parse("02:20") },
                new Stage { Number = "Stage 5", Name = "Great Kingshill", Cutoff = TimeSpan.Parse("02:32") },
                new Stage { Number = "Stage 6", Name = "Chipperfield", Cutoff = TimeSpan.Parse("01:31") },
                new Stage { Number = "Stage 7", Name = "St.Albans", Cutoff = TimeSpan.Parse("02:07") },
                new Stage { Number = "Stage 8", Name = "Letty Green", Cutoff = TimeSpan.Parse("01:55") },
                new Stage { Number = "Stage 9", Name = "Dobbs Weir", Cutoff = TimeSpan.Parse("01:52") },
                new Stage { Number = "Stage 10", Name = "High Beach", Cutoff = TimeSpan.Parse("01:38") },
                new Stage { Number = "Stage 11", Name = "Toot Hill", Cutoff = TimeSpan.Parse("01:20") },
                new Stage { Number = "Stage 12", Name = "Blackmore", Cutoff = TimeSpan.Parse("01:57") },
                new Stage { Number = "Stage 13", Name = "Thorndon Park", Cutoff = TimeSpan.Parse("01:11") },
                new Stage { Number = "Stage 14", Name = "Cranham", Cutoff = TimeSpan.Parse("01:34") },
                new Stage { Number = "Stage 15", Name = "Stone Lodge", Cutoff = TimeSpan.Parse("01:43") },
                new Stage { Number = "Stage 16", Name = "Lullingstone Park", Cutoff = TimeSpan.Parse("02:31") },
                new Stage { Number = "Stage 17", Name = "Tatsfield", Cutoff = TimeSpan.Parse("01:55") },
                new Stage { Number = "Stage 18", Name = "Merstham", Cutoff = TimeSpan.Parse("01:49") },
                new Stage { Number = "Stage 19", Name = "Burford Bridge", Cutoff = TimeSpan.Parse("01:32") },
                new Stage { Number = "Stage 20", Name = "West Hanger", Cutoff = TimeSpan.Parse("01:04") },
                new Stage { Number = "Stage 21", Name = "Ripley", Cutoff = TimeSpan.Parse("01:30") },
                new Stage { Number = "Stage 22", Name = "Walton Bridge", Cutoff = TimeSpan.Parse("01:20") }
            };
            foreach (Stage s in stages)
            {
                context.Stage.Add(s);
            }
            context.SaveChanges();

            //Categories
            if (context.Category.Any())
            {
                return;   // DB has been seeded
            }
            var category = new Category[]
            {
                new Category { Name = "Senior Men" },
                new Category { Name = "Senior Women" },
                new Category { Name = "V40 Men" },
                new Category { Name = "V35 Women" },
                new Category { Name = "V50 Men" },
                new Category { Name = "V45 Women" }
            };
            foreach (Category s in category)
            {
                context.Category.Add(s);
            }
            context.SaveChanges();

            //TeamCategories
            if (context.TeamCategory.Any())
            {
                return;   // DB has been seeded
            }
            var teamcat = new TeamCategory[]
            {
                new TeamCategory { Name = "Open Men" },
                new TeamCategory { Name = "Open Women" },
                new TeamCategory { Name = "Open Mixed" },
                new TeamCategory { Name = "Veteran Men" },
                new TeamCategory { Name = "Veteran Women" },
                new TeamCategory { Name = "Veteran Mixed" }
            };
            foreach (TeamCategory s in teamcat)
            {
                context.TeamCategory.Add(s);
            }
            context.SaveChanges();

            //Records
            if (context.Record.Any())
            {
                return;   // DB has been seeded
            }
            var records = new Record[]
            {
                new Record { Time = TimeSpan.Parse("01:10:09"), StageId = 1, CategoryId = 1 },
                new Record { Time = TimeSpan.Parse("01:13:04"), StageId = 1, CategoryId = 2 },
                new Record { Time = TimeSpan.Parse("01:13:04"), StageId = 1, CategoryId = 4 },
                new Record { Time = TimeSpan.Parse("01:14:43"), StageId = 1, CategoryId = 3 },
                new Record { Time = TimeSpan.Parse("01:26:41"), StageId = 1, CategoryId = 6 },
                new Record { Time = TimeSpan.Parse("01:17:21"), StageId = 1, CategoryId = 5 },
                new Record { Time = TimeSpan.Parse("00:55:36"), StageId = 2, CategoryId = 1 },
                new Record { Time = TimeSpan.Parse("01:00:34"), StageId = 2, CategoryId = 2 },
                new Record { Time = TimeSpan.Parse("01:00:34"), StageId = 2, CategoryId = 4 },
                new Record { Time = TimeSpan.Parse("00:56:14"), StageId = 2, CategoryId = 3 },
                new Record { Time = TimeSpan.Parse("01:07:23"), StageId = 2, CategoryId = 6 },
                new Record { Time = TimeSpan.Parse("01:00:22"), StageId = 2, CategoryId = 5 },
                new Record { Time = TimeSpan.Parse("01:04:12"), StageId = 3, CategoryId = 1 },
                new Record { Time = TimeSpan.Parse("01:18:38"), StageId = 3, CategoryId = 2 },
                new Record { Time = TimeSpan.Parse("01:22:49"), StageId = 3, CategoryId = 4 },
                new Record { Time = TimeSpan.Parse("01:06:23"), StageId = 3, CategoryId = 3 },
                new Record { Time = TimeSpan.Parse("01:22:49"), StageId = 3, CategoryId = 6 },
                new Record { Time = TimeSpan.Parse("01:17:30"), StageId = 3, CategoryId = 5 },
                new Record { Time = TimeSpan.Parse("01:11:10"), StageId = 4, CategoryId = 1 },
                new Record { Time = TimeSpan.Parse("01:23:21"), StageId = 4, CategoryId = 2 },
                new Record { Time = TimeSpan.Parse("01:26:52"), StageId = 4, CategoryId = 4 },
                new Record { Time = TimeSpan.Parse("01:14:29"), StageId = 4, CategoryId = 3 },
                new Record { Time = TimeSpan.Parse("01:32:44"), StageId = 4, CategoryId = 6 },
                new Record { Time = TimeSpan.Parse("01:18:02"), StageId = 4, CategoryId = 5 },
                new Record { Time = TimeSpan.Parse("01:13:54"), StageId = 5, CategoryId = 1 },
                new Record { Time = TimeSpan.Parse("01:29:44"), StageId = 5, CategoryId = 2 },
                new Record { Time = TimeSpan.Parse("01:32:30"), StageId = 5, CategoryId = 4 },
                new Record { Time = TimeSpan.Parse("01:20:54"), StageId = 5, CategoryId = 3 },
                new Record { Time = TimeSpan.Parse("01:42:48"), StageId = 5, CategoryId = 6 },
                new Record { Time = TimeSpan.Parse("01:31:45"), StageId = 5, CategoryId = 5 },
                new Record { Time = TimeSpan.Parse("00:50:06"), StageId = 6, CategoryId = 1 },
                new Record { Time = TimeSpan.Parse("00:55:29"), StageId = 6, CategoryId = 2 },
                new Record { Time = TimeSpan.Parse("00:55:29"), StageId = 6, CategoryId = 4 },
                new Record { Time = TimeSpan.Parse("00:50:06"), StageId = 6, CategoryId = 3 },
                new Record { Time = TimeSpan.Parse("00:59:29"), StageId = 6, CategoryId = 6 },
                new Record { Time = TimeSpan.Parse("01:01:39"), StageId = 6, CategoryId = 5 },
                new Record { Time = TimeSpan.Parse("01:02:21"), StageId = 7, CategoryId = 1 },
                new Record { Time = TimeSpan.Parse("01:14:05"), StageId = 7, CategoryId = 2 },
                new Record { Time = TimeSpan.Parse("01:15:39"), StageId = 7, CategoryId = 4 },
                new Record { Time = TimeSpan.Parse("01:06:19"), StageId = 7, CategoryId = 3 },
                new Record { Time = TimeSpan.Parse("01:26:57"), StageId = 7, CategoryId = 6 },
                new Record { Time = TimeSpan.Parse("01:09:44"), StageId = 7, CategoryId = 5 },
                new Record { Time = TimeSpan.Parse("00:58:02"), StageId = 8, CategoryId = 1 },
                new Record { Time = TimeSpan.Parse("01:03:51"), StageId = 8, CategoryId = 2 },
                new Record { Time = TimeSpan.Parse("01:08:23"), StageId = 8, CategoryId = 4 },
                new Record { Time = TimeSpan.Parse("01:00:10"), StageId = 8, CategoryId = 3 },
                new Record { Time = TimeSpan.Parse("01:12:02"), StageId = 8, CategoryId = 6 },
                new Record { Time = TimeSpan.Parse("01:05:53"), StageId = 8, CategoryId = 5 },
                new Record { Time = TimeSpan.Parse("00:58:23"), StageId = 9, CategoryId = 1 },
                new Record { Time = TimeSpan.Parse("01:05:33"), StageId = 9, CategoryId = 2 },
                new Record { Time = TimeSpan.Parse("01:07:50"), StageId = 9, CategoryId = 4 },
                new Record { Time = TimeSpan.Parse("00:58:23"), StageId = 9, CategoryId = 3 },
                new Record { Time = TimeSpan.Parse("01:13:54"), StageId = 9, CategoryId = 6 },
                new Record { Time = TimeSpan.Parse("01:05:13"), StageId = 9, CategoryId = 5 },
                new Record { Time = TimeSpan.Parse("00:54:25"), StageId = 10, CategoryId = 1 },
                new Record { Time = TimeSpan.Parse("01:06:05"), StageId = 10, CategoryId = 2 },
                new Record { Time = TimeSpan.Parse("01:06:05"), StageId = 10, CategoryId = 4 },
                new Record { Time = TimeSpan.Parse("00:58:37"), StageId = 10, CategoryId = 3 },
                new Record { Time = TimeSpan.Parse("01:07:18"), StageId = 10, CategoryId = 6 },
                new Record { Time = TimeSpan.Parse("01:01:49"), StageId = 10, CategoryId = 5 },
                new Record { Time = TimeSpan.Parse("00:43:22"), StageId = 11, CategoryId = 1 },
                new Record { Time = TimeSpan.Parse("00:46:49"), StageId = 11, CategoryId = 2 },
                new Record { Time = TimeSpan.Parse("00:46:49"), StageId = 11, CategoryId = 4 },
                new Record { Time = TimeSpan.Parse("00:44:15"), StageId = 11, CategoryId = 3 },
                new Record { Time = TimeSpan.Parse("00:53:40"), StageId = 11, CategoryId = 6 },
                new Record { Time = TimeSpan.Parse("00:48:34"), StageId = 11, CategoryId = 5 },
                new Record { Time = TimeSpan.Parse("00:59:36"), StageId = 12, CategoryId = 1 },
                new Record { Time = TimeSpan.Parse("01:08:39"), StageId = 12, CategoryId = 2 },
                new Record { Time = TimeSpan.Parse("01:08:39"), StageId = 12, CategoryId = 4 },
                new Record { Time = TimeSpan.Parse("01:02:38"), StageId = 12, CategoryId = 3 },
                new Record { Time = TimeSpan.Parse("01:17:26"), StageId = 12, CategoryId = 6 },
                new Record { Time = TimeSpan.Parse("01:05:23"), StageId = 12, CategoryId = 5 },
                new Record { Time = TimeSpan.Parse("00:35:38"), StageId = 13, CategoryId = 1 },
                new Record { Time = TimeSpan.Parse("00:41:58"), StageId = 13, CategoryId = 2 },
                new Record { Time = TimeSpan.Parse("00:45:29"), StageId = 13, CategoryId = 4 },
                new Record { Time = TimeSpan.Parse("00:38:36"), StageId = 13, CategoryId = 3 },
                new Record { Time = TimeSpan.Parse("00:47:08"), StageId = 13, CategoryId = 6 },
                new Record { Time = TimeSpan.Parse("00:40:04"), StageId = 13, CategoryId = 5 },
                new Record { Time = TimeSpan.Parse("00:53:39"), StageId = 14, CategoryId = 1 },
                new Record { Time = TimeSpan.Parse("01:07:24"), StageId = 14, CategoryId = 2 },
                new Record { Time = TimeSpan.Parse("01:16:41"), StageId = 14, CategoryId = 4 },
                new Record { Time = TimeSpan.Parse("00:59:01"), StageId = 14, CategoryId = 3 },
                new Record { Time = TimeSpan.Parse("01:16:41"), StageId = 14, CategoryId = 6 },
                new Record { Time = TimeSpan.Parse("01:02:52"), StageId = 14, CategoryId = 5 },
                new Record { Time = TimeSpan.Parse("00:53:17"), StageId = 15, CategoryId = 1 },
                new Record { Time = TimeSpan.Parse("01:05:45"), StageId = 15, CategoryId = 2 },
                new Record { Time = TimeSpan.Parse("01:05:45"), StageId = 15, CategoryId = 4 },
                new Record { Time = TimeSpan.Parse("01:03:07"), StageId = 15, CategoryId = 3 },
                new Record { Time = TimeSpan.Parse("01:11:17"), StageId = 15, CategoryId = 6 },
                new Record { Time = TimeSpan.Parse("01:03:07"), StageId = 15, CategoryId = 5 },
                new Record { Time = TimeSpan.Parse("01:15:48"), StageId = 16, CategoryId = 1 },
                new Record { Time = TimeSpan.Parse("01:29:28"), StageId = 16, CategoryId = 2 },
                new Record { Time = TimeSpan.Parse("01:29:58"), StageId = 16, CategoryId = 4 },
                new Record { Time = TimeSpan.Parse("01:19:41"), StageId = 16, CategoryId = 3 },
                new Record { Time = TimeSpan.Parse("01:39:42"), StageId = 16, CategoryId = 6 },
                new Record { Time = TimeSpan.Parse("01:28:53"), StageId = 16, CategoryId = 5 },
                new Record { Time = TimeSpan.Parse("00:55:08"), StageId = 17, CategoryId = 1 },
                new Record { Time = TimeSpan.Parse("01:05:28"), StageId = 17, CategoryId = 2 },
                new Record { Time = TimeSpan.Parse("01:08:10"), StageId = 17, CategoryId = 4 },
                new Record { Time = TimeSpan.Parse("00:59:24"), StageId = 17, CategoryId = 3 },
                new Record { Time = TimeSpan.Parse("01:14:49"), StageId = 17, CategoryId = 6 },
                new Record { Time = TimeSpan.Parse("01:04:27"), StageId = 17, CategoryId = 5 },
                new Record { Time = TimeSpan.Parse("01:00:22"), StageId = 18, CategoryId = 1 },
                new Record { Time = TimeSpan.Parse("01:07:36"), StageId = 18, CategoryId = 2 },
                new Record { Time = TimeSpan.Parse("01:15:39"), StageId = 18, CategoryId = 4 },
                new Record { Time = TimeSpan.Parse("01:00:22"), StageId = 18, CategoryId = 3 },
                new Record { Time = TimeSpan.Parse("01:30:10"), StageId = 18, CategoryId = 6 },
                new Record { Time = TimeSpan.Parse("01:10:49"), StageId = 18, CategoryId = 5 },
                new Record { Time = TimeSpan.Parse("00:53:02"), StageId = 19, CategoryId = 1 },
                new Record { Time = TimeSpan.Parse("01:02:00"), StageId = 19, CategoryId = 2 },
                new Record { Time = TimeSpan.Parse("01:02:54"), StageId = 19, CategoryId = 4 },
                new Record { Time = TimeSpan.Parse("00:55:48"), StageId = 19, CategoryId = 3 },
                new Record { Time = TimeSpan.Parse("01:03:19"), StageId = 19, CategoryId = 6 },
                new Record { Time = TimeSpan.Parse("00:59:35"), StageId = 19, CategoryId = 5 },
                new Record { Time = TimeSpan.Parse("00:31:07"), StageId = 20, CategoryId = 1 },
                new Record { Time = TimeSpan.Parse("00:35:32"), StageId = 20, CategoryId = 2 },
                new Record { Time = TimeSpan.Parse("00:37:32"), StageId = 20, CategoryId = 4 },
                new Record { Time = TimeSpan.Parse("00:32:30"), StageId = 20, CategoryId = 3 },
                new Record { Time = TimeSpan.Parse("00:40:01"), StageId = 20, CategoryId = 6 },
                new Record { Time = TimeSpan.Parse("00:39:07"), StageId = 20, CategoryId = 5 },
                new Record { Time = TimeSpan.Parse("00:46:41"), StageId = 21, CategoryId = 1 },
                new Record { Time = TimeSpan.Parse("00:54:10"), StageId = 21, CategoryId = 2 },
                new Record { Time = TimeSpan.Parse("00:54:10"), StageId = 21, CategoryId = 4 },
                new Record { Time = TimeSpan.Parse("00:47:59"), StageId = 21, CategoryId = 3 },
                new Record { Time = TimeSpan.Parse("00:55:57"), StageId = 21, CategoryId = 6 },
                new Record { Time = TimeSpan.Parse("00:53:02"), StageId = 21, CategoryId = 5 },
                new Record { Time = TimeSpan.Parse("00:39:10"), StageId = 22, CategoryId = 1 },
                new Record { Time = TimeSpan.Parse("00:47:15"), StageId = 22, CategoryId = 2 },
                new Record { Time = TimeSpan.Parse("00:47:15"), StageId = 22, CategoryId = 4 },
                new Record { Time = TimeSpan.Parse("00:39:10"), StageId = 22, CategoryId = 3 },
                new Record { Time = TimeSpan.Parse("00:54:20"), StageId = 22, CategoryId = 6 },
                new Record { Time = TimeSpan.Parse("00:44:53"), StageId = 22, CategoryId = 5 }
            };
            foreach (Record s in records)
            {
                context.Record.Add(s);
            }
            context.SaveChanges();

            //Captains
            if (context.Captain.Any())
            {
                return;   // DB has been seeded
            }
            var captain = new Captain[]
            {
                new Captain { Name = "contact@richardwkirk.co.uk" },
                new Captain { Name = "jonlear@hotmail.co.uk" },
                new Captain { Name = "fletcherpaul90@gmail.com" },
                new Captain { Name = "shenton.simon@bcg.com" },
                new Captain { Name = "christkelly@yahoo.com" },
                new Captain { Name = "j.wadey@sky.com" },
                new Captain { Name = "joespraggins@hotmail.co.uk" },
                new Captain { Name = "races@claphampioneers.co.uk" },
                new Captain { Name = "claphamrunners@gmail.com" },
                new Captain { Name = "angenorris@googlemail.com" },
                new Captain { Name = "angeladuff81@gmail.com" },
                new Captain { Name = "richnicholson@gmail.com" },
                new Captain { Name = "simoncornish@mac.com" },
                new Captain { Name = "bf4kg@icloud.com" },
                new Captain { Name = "andywood3@hotmail.com" },
                new Captain { Name = "julianandroman@me.com" },
                new Captain { Name = "nick_sille@hotmail.com" },
                new Captain { Name = "TimKLewin@gmail.com" },
                new Captain { Name = "ranelaghgbr@gmail.com" },
                new Captain { Name = "hanssale4@yahoo.co.uk" },
                new Captain { Name = "ian.fullen@hotmail.com" },
                new Captain { Name = "simonwebb79@gmail.com" },
                new Captain { Name = "brynreynolds1@hotmail.com" },
                new Captain { Name = "zoe.a.riding@gmail.com " },
                new Captain { Name = "m4ttjones@yahoo.co.uk" },
                new Captain { Name = "johnpetermac@hotmail.com" },
                new Captain { Name = "philreeves@outlook.com" }
            };
            var captainRole = new string[] { "Captain" };
            foreach (Captain s in captain)
            {
                context.Captain.Add(s);
                context.SaveChanges();
            }


            //TeamCreate
            if (context.Team.Any())
            {
                return;   // DB has been seeded
            }
            var team = new BulkTeamCreateViewModel[]
            {
                new BulkTeamCreateViewModel { StartNumber = 0, TeamName = "26.2 RRC 1", CaptainEmail = "contact@richardwkirk.co.uk" },
                new BulkTeamCreateViewModel { StartNumber = 15, TeamName = "26.2 RRC 2", CaptainEmail = "contact@richardwkirk.co.uk" },
                new BulkTeamCreateViewModel { StartNumber = 30, TeamName = "26.2 RRC 3", CaptainEmail = "contact@richardwkirk.co.uk" },
                new BulkTeamCreateViewModel { StartNumber = 45, TeamName = "BeaRCat Running Club 1", CaptainEmail = "jonlear@hotmail.co.uk" },
                new BulkTeamCreateViewModel { StartNumber = 60, TeamName = "BeaRCat Running Club 2", CaptainEmail = "jonlear@hotmail.co.uk" },
                new BulkTeamCreateViewModel { StartNumber = 75, TeamName = "Beckenham Running Club 1", CaptainEmail = "fletcherpaul90@gmail.com" },
                new BulkTeamCreateViewModel { StartNumber = 90, TeamName = "Beckenham Running Club 2", CaptainEmail = "fletcherpaul90@gmail.com" },
                new BulkTeamCreateViewModel { StartNumber = 105, TeamName = "Boston Consulting Group 1", CaptainEmail = "shenton.simon@bcg.com" },
                new BulkTeamCreateViewModel { StartNumber = 120, TeamName = "Boston Consulting Group 2", CaptainEmail = "shenton.simon@bcg.com" },
                new BulkTeamCreateViewModel { StartNumber = 135, TeamName = "British Airways AC 1", CaptainEmail = "christkelly@yahoo.com" },
                new BulkTeamCreateViewModel { StartNumber = 150, TeamName = "Burgess Hill Runners 1", CaptainEmail = "j.wadey@sky.com" },
                new BulkTeamCreateViewModel { StartNumber = 165, TeamName = "Burgess Hill Runners 2", CaptainEmail = "j.wadey@sky.com" },
                new BulkTeamCreateViewModel { StartNumber = 180, TeamName = "Burgess Hill Runners 3", CaptainEmail = "j.wadey@sky.com" },
                new BulkTeamCreateViewModel { StartNumber = 195, TeamName = "Clapham Chasers 1", CaptainEmail = "joespraggins@hotmail.co.uk" },
                new BulkTeamCreateViewModel { StartNumber = 210, TeamName = "Clapham Chasers 2", CaptainEmail = "joespraggins@hotmail.co.uk" },
                new BulkTeamCreateViewModel { StartNumber = 225, TeamName = "Clapham Chasers 3", CaptainEmail = "joespraggins@hotmail.co.uk" },
                new BulkTeamCreateViewModel { StartNumber = 240, TeamName = "Clapham Pioneers 1", CaptainEmail = "races@claphampioneers.co.uk" },
                new BulkTeamCreateViewModel { StartNumber = 255, TeamName = "Clapham Pioneers 2", CaptainEmail = "races@claphampioneers.co.uk" },
                new BulkTeamCreateViewModel { StartNumber = 270, TeamName = "Clapham Runners 1", CaptainEmail = "claphamrunners@gmail.com" },
                new BulkTeamCreateViewModel { StartNumber = 285, TeamName = "Dulwich Runners 1", CaptainEmail = "angenorris@googlemail.com" },
                new BulkTeamCreateViewModel { StartNumber = 300, TeamName = "Dulwich Runners 2", CaptainEmail = "angenorris@googlemail.com" },
                new BulkTeamCreateViewModel { StartNumber = 315, TeamName = "Ealing Eagles 1", CaptainEmail = "angeladuff81@gmail.com" },
                new BulkTeamCreateViewModel { StartNumber = 330, TeamName = "Ealing Eagles 2", CaptainEmail = "angeladuff81@gmail.com" },
                new BulkTeamCreateViewModel { StartNumber = 345, TeamName = "Ealing Eagles 3", CaptainEmail = "angeladuff81@gmail.com" },
                new BulkTeamCreateViewModel { StartNumber = 360, TeamName = "Elmbridge 1", CaptainEmail = "richnicholson@gmail.com" },
                new BulkTeamCreateViewModel { StartNumber = 375, TeamName = "Hampton Wick Wanderers 1", CaptainEmail = "simoncornish@mac.com" },
                new BulkTeamCreateViewModel { StartNumber = 390, TeamName = "Havering 90 Joggers 1", CaptainEmail = "bf4kg@icloud.com" },
                new BulkTeamCreateViewModel { StartNumber = 405, TeamName = "Hillingdon AC 1", CaptainEmail = "andywood3@hotmail.com" },
                new BulkTeamCreateViewModel { StartNumber = 420, TeamName = "London Front Runners 1", CaptainEmail = "julianandroman@me.com" },
                new BulkTeamCreateViewModel { StartNumber = 435, TeamName = "London Front Runners 2", CaptainEmail = "julianandroman@me.com" },
                new BulkTeamCreateViewModel { StartNumber = 450, TeamName = "London Front Runners 3", CaptainEmail = "julianandroman@me.com" },
                new BulkTeamCreateViewModel { StartNumber = 465, TeamName = "London Front Runners 4", CaptainEmail = "julianandroman@me.com" },
                new BulkTeamCreateViewModel { StartNumber = 480, TeamName = "Maidenhead AC 1", CaptainEmail = "nick_sille@hotmail.com" },
                new BulkTeamCreateViewModel { StartNumber = 495, TeamName = "Maidenhead AC 2", CaptainEmail = "nick_sille@hotmail.com" },
                new BulkTeamCreateViewModel { StartNumber = 510, TeamName = "Queens Park Harriers 1", CaptainEmail = "TimKLewin@gmail.com" },
                new BulkTeamCreateViewModel { StartNumber = 525, TeamName = "Ranelagh Harriers 1", CaptainEmail = "ranelaghgbr@gmail.com" },
                new BulkTeamCreateViewModel { StartNumber = 540, TeamName = "Ranelagh Harriers 2", CaptainEmail = "ranelaghgbr@gmail.com" },
                new BulkTeamCreateViewModel { StartNumber = 555, TeamName = "Ranelagh Harriers 3", CaptainEmail = "ranelaghgbr@gmail.com" },
                new BulkTeamCreateViewModel { StartNumber = 570, TeamName = "Serpentine 1", CaptainEmail = "hanssale4@yahoo.co.uk" },
                new BulkTeamCreateViewModel { StartNumber = 585, TeamName = "Serpentine 2", CaptainEmail = "hanssale4@yahoo.co.uk" },
                new BulkTeamCreateViewModel { StartNumber = 600, TeamName = "Serpentine 3", CaptainEmail = "hanssale4@yahoo.co.uk" },
                new BulkTeamCreateViewModel { StartNumber = 615, TeamName = "SHAEF Shifters 1", CaptainEmail = "ian.fullen@hotmail.com" },
                new BulkTeamCreateViewModel { StartNumber = 630, TeamName = "Stragglers 1", CaptainEmail = "simonwebb79@gmail.com" },
                new BulkTeamCreateViewModel { StartNumber = 645, TeamName = "Stragglers 2", CaptainEmail = "simonwebb79@gmail.com" },
                new BulkTeamCreateViewModel { StartNumber = 660, TeamName = "Sutton Striders 1", CaptainEmail = "brynreynolds1@hotmail.com" },
                new BulkTeamCreateViewModel { StartNumber = 675, TeamName = "Sutton Striders 2", CaptainEmail = "brynreynolds1@hotmail.com" },
                new BulkTeamCreateViewModel { StartNumber = 690, TeamName = "Team Bushy 1", CaptainEmail = "zoe.a.riding@gmail.com " },
                new BulkTeamCreateViewModel { StartNumber = 705, TeamName = "Thames Hare & Hounds 1", CaptainEmail = "m4ttjones@yahoo.co.uk" },
                new BulkTeamCreateViewModel { StartNumber = 720, TeamName = "Thames Hare & Hounds 2", CaptainEmail = "m4ttjones@yahoo.co.uk" },
                new BulkTeamCreateViewModel { StartNumber = 735, TeamName = "Waverley Harriers 1", CaptainEmail = "johnpetermac@hotmail.com" },
                new BulkTeamCreateViewModel { StartNumber = 750, TeamName = "Windmilers 1", CaptainEmail = "philreeves@outlook.com" },

            };
            foreach (BulkTeamCreateViewModel s in team)
            {
                var captainId = context.Captain
                    .Where(u => u.Name == s.CaptainEmail)
                    .FirstOrDefault();

                var newTeam = new Team
                {
                    TeamCategoryId = 1,
                    Name = s.TeamName,
                    CaptainId = captainId.CaptainId
                };
                context.Team.Add(newTeam);
                context.SaveChanges();

                for (int i = 0; i < 15; i++)
                {
                    var num = s.StartNumber + i;
                    var newBibNumber = new BibNumber
                    {
                        Name = num.ToString(),
                        TeamId = newTeam.TeamId
                    };

                    context.Add(newBibNumber);
                    context.SaveChanges();
                }
            }


        }

        internal static async Task SeedAdminUser(AppIdentityDbContext context)
        {

            string email = "allangbarrie@gmail.com";

            var user = new AppUser
            {
                UserName = email,
                NormalizedUserName = email.ToUpper(),
                Email = email,
                NormalizedEmail = email.ToUpper(),
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var roleStore = new RoleStore<IdentityRole>(context);

            if (!context.Roles.Any(r => r.Name == "Administrator"))
            {
                await roleStore.CreateAsync(new IdentityRole { Name = "Administrator", NormalizedName = "ADMINISTRATOR" });
            }

            if (!context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<AppUser>();
                var hashed = password.HashPassword(user, "Wxva4uahKS3uei5");
                user.PasswordHash = hashed;
                var userStore = new UserStore<AppUser>(context);
                await userStore.CreateAsync(user);
                await userStore.AddToRoleAsync(user, "Administrator");
            }

            await context.SaveChangesAsync();
        }

        internal static async Task SeedPeter(AppIdentityDbContext context)
        {

            string email = "greenbeltrelay@outlook.com";

            var user = new AppUser
            {
                UserName = email,
                NormalizedUserName = email.ToUpper(),
                Email = email,
                NormalizedEmail = email.ToUpper(),
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var roleStore = new RoleStore<IdentityRole>(context);

            if (!context.Roles.Any(r => r.Name == "Administrator"))
            {
                await roleStore.CreateAsync(new IdentityRole { Name = "Administrator", NormalizedName = "ADMINISTRATOR" });
            }

            if (!context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<AppUser>();
                var hashed = password.HashPassword(user, "Wxva4uahKS3uei5");
                user.PasswordHash = hashed;
                var userStore = new UserStore<AppUser>(context);
                await userStore.CreateAsync(user);
                await userStore.AddToRoleAsync(user, "Administrator");
            }

            await context.SaveChangesAsync();
        }

        internal static async Task SeedCaptains(AppIdentityDbContext context)
        {

            var team = new BulkTeamCreateViewModel[]
            {
                new BulkTeamCreateViewModel { StartNumber = 0, TeamName = "26.2 RRC 1", CaptainEmail = "contact@richardwkirk.co.uk" },
                new BulkTeamCreateViewModel { StartNumber = 15, TeamName = "26.2 RRC 2", CaptainEmail = "contact@richardwkirk.co.uk" },
                new BulkTeamCreateViewModel { StartNumber = 30, TeamName = "26.2 RRC 3", CaptainEmail = "contact@richardwkirk.co.uk" },
                new BulkTeamCreateViewModel { StartNumber = 45, TeamName = "BeaRCat Running Club 1", CaptainEmail = "jonlear@hotmail.co.uk" },
                new BulkTeamCreateViewModel { StartNumber = 60, TeamName = "BeaRCat Running Club 2", CaptainEmail = "jonlear@hotmail.co.uk" },
                new BulkTeamCreateViewModel { StartNumber = 75, TeamName = "Beckenham Running Club 1", CaptainEmail = "fletcherpaul90@gmail.com" },
                new BulkTeamCreateViewModel { StartNumber = 90, TeamName = "Beckenham Running Club 2", CaptainEmail = "fletcherpaul90@gmail.com" },
                new BulkTeamCreateViewModel { StartNumber = 105, TeamName = "Boston Consulting Group 1", CaptainEmail = "shenton.simon@bcg.com" },
                new BulkTeamCreateViewModel { StartNumber = 120, TeamName = "Boston Consulting Group 2", CaptainEmail = "shenton.simon@bcg.com" },
                new BulkTeamCreateViewModel { StartNumber = 135, TeamName = "British Airways AC 1", CaptainEmail = "christkelly@yahoo.com" },
                new BulkTeamCreateViewModel { StartNumber = 150, TeamName = "Burgess Hill Runners 1", CaptainEmail = "j.wadey@sky.com" },
                new BulkTeamCreateViewModel { StartNumber = 165, TeamName = "Burgess Hill Runners 2", CaptainEmail = "j.wadey@sky.com" },
                new BulkTeamCreateViewModel { StartNumber = 180, TeamName = "Burgess Hill Runners 3", CaptainEmail = "j.wadey@sky.com" },
                new BulkTeamCreateViewModel { StartNumber = 195, TeamName = "Clapham Chasers 1", CaptainEmail = "joespraggins@hotmail.co.uk" },
                new BulkTeamCreateViewModel { StartNumber = 210, TeamName = "Clapham Chasers 2", CaptainEmail = "joespraggins@hotmail.co.uk" },
                new BulkTeamCreateViewModel { StartNumber = 225, TeamName = "Clapham Chasers 3", CaptainEmail = "joespraggins@hotmail.co.uk" },
                new BulkTeamCreateViewModel { StartNumber = 240, TeamName = "Clapham Pioneers 1", CaptainEmail = "races@claphampioneers.co.uk" },
                new BulkTeamCreateViewModel { StartNumber = 255, TeamName = "Clapham Pioneers 2", CaptainEmail = "races@claphampioneers.co.uk" },
                new BulkTeamCreateViewModel { StartNumber = 270, TeamName = "Clapham Runners 1", CaptainEmail = "claphamrunners@gmail.com" },
                new BulkTeamCreateViewModel { StartNumber = 285, TeamName = "Dulwich Runners 1", CaptainEmail = "angenorris@googlemail.com" },
                new BulkTeamCreateViewModel { StartNumber = 300, TeamName = "Dulwich Runners 2", CaptainEmail = "angenorris@googlemail.com" },
                new BulkTeamCreateViewModel { StartNumber = 315, TeamName = "Ealing Eagles 1", CaptainEmail = "angeladuff81@gmail.com" },
                new BulkTeamCreateViewModel { StartNumber = 330, TeamName = "Ealing Eagles 2", CaptainEmail = "angeladuff81@gmail.com" },
                new BulkTeamCreateViewModel { StartNumber = 345, TeamName = "Ealing Eagles 3", CaptainEmail = "angeladuff81@gmail.com" },
                new BulkTeamCreateViewModel { StartNumber = 360, TeamName = "Elmbridge 1", CaptainEmail = "richnicholson@gmail.com" },
                new BulkTeamCreateViewModel { StartNumber = 375, TeamName = "Hampton Wick Wanderers 1", CaptainEmail = "simoncornish@mac.com" },
                new BulkTeamCreateViewModel { StartNumber = 390, TeamName = "Havering 90 Joggers 1", CaptainEmail = "bf4kg@icloud.com" },
                new BulkTeamCreateViewModel { StartNumber = 405, TeamName = "Hillingdon AC 1", CaptainEmail = "andywood3@hotmail.com" },
                new BulkTeamCreateViewModel { StartNumber = 420, TeamName = "London Front Runners 1", CaptainEmail = "julianandroman@me.com" },
                new BulkTeamCreateViewModel { StartNumber = 435, TeamName = "London Front Runners 2", CaptainEmail = "julianandroman@me.com" },
                new BulkTeamCreateViewModel { StartNumber = 450, TeamName = "London Front Runners 3", CaptainEmail = "julianandroman@me.com" },
                new BulkTeamCreateViewModel { StartNumber = 465, TeamName = "London Front Runners 4", CaptainEmail = "julianandroman@me.com" },
                new BulkTeamCreateViewModel { StartNumber = 480, TeamName = "Maidenhead AC 1", CaptainEmail = "nick_sille@hotmail.com" },
                new BulkTeamCreateViewModel { StartNumber = 495, TeamName = "Maidenhead AC 2", CaptainEmail = "nick_sille@hotmail.com" },
                new BulkTeamCreateViewModel { StartNumber = 510, TeamName = "Queens Park Harriers 1", CaptainEmail = "TimKLewin@gmail.com" },
                new BulkTeamCreateViewModel { StartNumber = 525, TeamName = "Ranelagh Harriers 1", CaptainEmail = "ranelaghgbr@gmail.com" },
                new BulkTeamCreateViewModel { StartNumber = 540, TeamName = "Ranelagh Harriers 2", CaptainEmail = "ranelaghgbr@gmail.com" },
                new BulkTeamCreateViewModel { StartNumber = 555, TeamName = "Ranelagh Harriers 3", CaptainEmail = "ranelaghgbr@gmail.com" },
                new BulkTeamCreateViewModel { StartNumber = 570, TeamName = "Serpentine 1", CaptainEmail = "hanssale4@yahoo.co.uk" },
                new BulkTeamCreateViewModel { StartNumber = 585, TeamName = "Serpentine 2", CaptainEmail = "hanssale4@yahoo.co.uk" },
                new BulkTeamCreateViewModel { StartNumber = 600, TeamName = "Serpentine 3", CaptainEmail = "hanssale4@yahoo.co.uk" },
                new BulkTeamCreateViewModel { StartNumber = 615, TeamName = "SHAEF Shifters 1", CaptainEmail = "ian.fullen@hotmail.com" },
                new BulkTeamCreateViewModel { StartNumber = 630, TeamName = "Stragglers 1", CaptainEmail = "simonwebb79@gmail.com" },
                new BulkTeamCreateViewModel { StartNumber = 645, TeamName = "Stragglers 2", CaptainEmail = "simonwebb79@gmail.com" },
                new BulkTeamCreateViewModel { StartNumber = 660, TeamName = "Sutton Striders 1", CaptainEmail = "brynreynolds1@hotmail.com" },
                new BulkTeamCreateViewModel { StartNumber = 675, TeamName = "Sutton Striders 2", CaptainEmail = "brynreynolds1@hotmail.com" },
                new BulkTeamCreateViewModel { StartNumber = 690, TeamName = "Team Bushy 1", CaptainEmail = "zoe.a.riding@gmail.com " },
                new BulkTeamCreateViewModel { StartNumber = 705, TeamName = "Thames Hare & Hounds 1", CaptainEmail = "m4ttjones@yahoo.co.uk" },
                new BulkTeamCreateViewModel { StartNumber = 720, TeamName = "Thames Hare & Hounds 2", CaptainEmail = "m4ttjones@yahoo.co.uk" },
                new BulkTeamCreateViewModel { StartNumber = 735, TeamName = "Waverley Harriers 1", CaptainEmail = "johnpetermac@hotmail.com" },
                new BulkTeamCreateViewModel { StartNumber = 750, TeamName = "Windmilers 1", CaptainEmail = "philreeves@outlook.com" },

            };
            foreach (BulkTeamCreateViewModel s in team)
            {
                var user = new AppUser
                {
                    UserName = s.CaptainEmail,
                    NormalizedUserName = s.CaptainEmail.ToUpper(),
                    Email = s.CaptainEmail,
                    NormalizedEmail = s.CaptainEmail.ToUpper(),
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                var roleStore = new RoleStore<IdentityRole>(context);

                if (!context.Roles.Any(r => r.Name == "Captain"))
                {
                    await roleStore.CreateAsync(new IdentityRole { Name = "Captain", NormalizedName = "CAPTAIN" });
                }

                if (!context.Users.Any(u => u.UserName == user.UserName))
                {
                    var password = new PasswordHasher<AppUser>();
                    var hashed = password.HashPassword(user, "Wxva4uahKS3uei5");
                    user.PasswordHash = hashed;
                    var userStore = new UserStore<AppUser>(context);
                    await userStore.CreateAsync(user);
                    await userStore.AddToRoleAsync(user, "Captain");
                }

                await context.SaveChangesAsync();
            }

        }

        internal static async Task SeedRunners(AppIdentityDbContext context)
        {

            //Captains
            if (context.Runner.Any())
            {
                return;   // DB has been seeded
            }

            var bibNumbers = await context.BibNumber.ToListAsync();
            var catId = 1;

            foreach (var bibNumber in bibNumbers)
            {
                if (catId > 6)
                { catId = 1; }

                var runner = new Runner
                {
                    First = "First",
                    Last = "Last",
                    BibNumberId = bibNumber.BibNumberId,
                    TeamId = bibNumber.TeamId,
                    CategoryId = catId
                };

                context.Runner.Add(runner);
                await context.SaveChangesAsync();

                catId++;
            }
        }
    }
}
