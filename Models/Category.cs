using System.ComponentModel.DataAnnotations;

namespace WebAdminConsole.Models
{
    public class Category
    {
        [Required]
        public int CategoryId { get; set; }

        [Display(Name = "Category")]
        public string Name { get; set; }
    }
}
