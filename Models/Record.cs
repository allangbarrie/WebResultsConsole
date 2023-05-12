using System.ComponentModel.DataAnnotations;

namespace WebAdminConsole.Models
{
    public class Record
    {
        [Required]
        public int RecordId { get; set; }

        [DataType(DataType.Duration)]
        public TimeSpan Time { get; set; }

        public int StageId { get; set; }
        public int CategoryId { get; set; }

        public Category Category { get; set; }
        public Stage Stage { get; set; }
    }
}
