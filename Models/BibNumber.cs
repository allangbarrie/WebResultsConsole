using System.ComponentModel.DataAnnotations;

namespace WebAdminConsole.Models
{
    public class BibNumber
    {
        [Required]
        public int BibNumberId { get; set; }
        [Required]
        [Display(Name = "BibNumber")]
        public string? Name { get; set; }

        public int TeamId { get; set; }
        public Team? Team { get; set; }
    }
}
