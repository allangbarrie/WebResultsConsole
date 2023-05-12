using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace WebAdminConsole.Models
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options) 
        {
            Database.Migrate();

        }

        public DbSet<Stage> Stage { get; set; } = default!;

        public DbSet<Category> Category { get; set; } = default!;

        public DbSet<Record> Record { get; set; } = default!;

        public DbSet<LeaderBoard> LeaderBoard { get; set; } = default!;

        public DbSet<BibNumber> BibNumber { get; set; } = default!;

        public DbSet<Result> Result { get; set; } = default!;

        public DbSet<Runner> Runner { get; set; } = default!;

        public DbSet<Team> Team { get; set; } = default!;

        public DbSet<TeamCategory> TeamCategory { get; set; } = default!;

        public DbSet<Captain> Captain { get; set; } = default!;
    }
}