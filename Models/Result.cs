using System.ComponentModel.DataAnnotations;

namespace WebAdminConsole.Models
{
    public class Result
    {
        [Required]
        public int ResultId { get; set; }
        public int StageId { get; set; }
        [DataType(DataType.Duration)]
        public TimeSpan Time { get; set; }
        public int BibNumberId { get; set; }

        public Stage Stage { get; set; }
        public BibNumber BibNumber { get; set; }
    }
}
