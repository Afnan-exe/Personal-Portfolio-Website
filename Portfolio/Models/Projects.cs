
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models
{
    public class Projects
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "*Value is required")]
        [DataType(DataType.Text)]
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }

        [Required(ErrorMessage = "*Value is required")]
        [DataType(DataType.Text)]
        [Display(Name = "Project Description")]
        public string ProjectDescription { get; set; }

        [Required(ErrorMessage = "*Value is required")]
        [Display(Name = "Image")]
        public string Image { get; set; }

        [Required(ErrorMessage = "*Value is required")]
        [DataType(DataType.Text)]
        [Display(Name = "Project Link")]
        public string ProjectLink { get; set; }
    }
}
