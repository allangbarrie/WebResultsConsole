using WebAdminConsole.Models;

namespace WebAdminConsole.ViewModels
{
    public class LeaderBoardViewModel
    {
        public int? Position { get; set; }

        public TimeSpan Time { get; set; }

        public TimeSpan Difference { get; set; }

        public Team? Team { get; set; }

        public int? CatPosition { get; set; }

        public TimeSpan CatDifference { get; set; }
    }
}

