using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models
{
    public class Contact
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "*Value is required")]
        [DataType(DataType.Text)]
        [Display(Name = "Your Name")]
        public string Name { get; set; } 

        [Required(ErrorMessage = "*Value is required")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Your Email")]
        public string Email { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Hi Afnan...")]
        public string ExtraComment { get; set; }
    }
}
