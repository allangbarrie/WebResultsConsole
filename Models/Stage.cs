using System.ComponentModel.DataAnnotations;

namespace WebAdminConsole.Models
{
    public class Stage
    {
        [Required]
        public int StageId { get; set; }
        [Display(Name = "Stage")]
        public string? Number { get; set; }
        public string? Name { get; set; }
        [DataType(DataType.Duration)]
        public TimeSpan Cutoff { get; set; }
    }
}
