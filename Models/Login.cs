using System.ComponentModel.DataAnnotations;

namespace WebAdminConsole.Models
{
    public class Login
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public bool Remember { get; set; }
    }
}
