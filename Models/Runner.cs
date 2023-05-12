using System.ComponentModel.DataAnnotations;

namespace WebAdminConsole.Models
{
    public class Runner
    {
        [Required]
        public int RunnerId { get; set; }
        [Required]
        public string First { get; set; }
        [Required]
        public string Last { get; set; }

        public int? BibNumberId { get; set; }
        public int TeamId { get; set; }
        public int CategoryId { get; set; }

        public Team Teams { get; set; }
        public Category Category { get; set; }
        public BibNumber BibNumber { get; set; }
    }
}
