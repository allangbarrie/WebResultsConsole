using WebAdminConsole.Models;

namespace WebAdminConsole.ViewModels
{
    public class StageResultsViewModel
    {
        public int? Position { get; set; }

        public TimeSpan Time { get; set; }

        public TimeSpan Difference { get; set; }

        public BibNumber? BibNumber { get; set; }

        public Runner? Runner { get; set; }

        public int? CatPosition { get; set; }

        public TimeSpan CatDifference { get; set; }
    }
}
