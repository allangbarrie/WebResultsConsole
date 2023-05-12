using System.ComponentModel.DataAnnotations;

namespace WebAdminConsole.Models
{
    public class TeamCategory
    {
        [Required]
        public int TeamCategoryId { get; set; }

        [Display(Name = "Team Category")]
        public string Name { get; set; }
    }
}
