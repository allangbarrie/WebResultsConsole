using System.ComponentModel.DataAnnotations;

namespace WebAdminConsole.Models
{
    public class LeaderBoard
    {
        [Required]
        public int LeaderBoardId { get; set; }

        public int Position { get; set; }
        
        public TimeSpan Time { get; set; }
        public TimeSpan Difference { get; set; }
        public int CategoryPosition { get; set; }
        public TimeSpan CategoryDifference { get; set; }

        public int StageId { get; set; }
        public Stage? Stage { get; set; }

        public int TeamCategoryId { get; set; }
        public TeamCategory? TeamCategory { get; set; }

        public int TeamId { get; set; }
        public Team? Team { get; set; }
    }
}
