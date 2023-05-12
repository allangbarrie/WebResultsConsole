using System.ComponentModel.DataAnnotations;
using WebAdminConsole.Models;

namespace WebAdminConsole.ViewModels
{
    public class UploadResultViewModel
    {
        public int Position { get; set; }
        [Display(Name = "Team Count")]
        public int Count { get; set; }

        public int StageId { get; set; }
        public Stage Stage { get; set; }

        public int BibNumberId { get; set; }
        public BibNumber BibNumber { get; set; }

        public int RunnerId { get; set; }
        public Runner Runner { get; set; }

        public int TeamId { get; set; }
        public Team Team { get; set; }

        public TimeSpan Time { get; set; }
    }
}
