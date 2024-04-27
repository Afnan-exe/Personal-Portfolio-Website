using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models
{
    public class LoginRegister
    {
        [Key]
        public int id {get;set;}

        [DisplayName("Name")]
        [MinLength(3, ErrorMessage = "User Name Must Contain 3 Characters")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Password")]
        [MaxLength(25, ErrorMessage = "Password Must Be Smaller Than 25 Characters"), MinLength(7, ErrorMessage = "Password Must Contain 7 Characters")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Confirm Password")]
        [MaxLength(25, ErrorMessage = "Password Must Be Smaller Than 25 Characters"), MinLength(7, ErrorMessage = "Password Must Contain 7 Characters")]
        public string PasswordConfirmed { get; set; }

        [DataType(DataType.EmailAddress)]
        [DisplayName("Email")]
        public string Email { get; set; }
    }
}
