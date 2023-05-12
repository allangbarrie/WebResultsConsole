using System.ComponentModel.DataAnnotations;

namespace WebAdminConsole.Models
{
    public class Team
    {
        [Required]
        public int TeamId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(40, ErrorMessage = "Name cannot be longer than 40 characters.")]
        [Display(Name = "Team")]
        public string Name { get; set; }


        [Required]
        public int CaptainId { get; set; }
        public virtual Captain Captain { get; set; }

        [Required]
        public int TeamCategoryId { get; set; }
        public virtual TeamCategory TeamCategory { get; set; }
    }
}
