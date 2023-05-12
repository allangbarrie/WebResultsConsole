using System.ComponentModel.DataAnnotations;

namespace WebAdminConsole.Models
{
    public class Captain
    {
        [Required]
        public int CaptainId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
    }
}
