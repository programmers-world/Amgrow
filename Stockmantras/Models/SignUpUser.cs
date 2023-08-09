using System.ComponentModel.DataAnnotations;

namespace Stockmantras.Models
{
    public class SignUpUser
    {

        [Required(ErrorMessage = "Please Enter First Name")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please Enter Last Name")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }
        [Key]
        [Required(ErrorMessage = "Please Enter your Email Address")]
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Email Address is not valid.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter password.")]
        [Display(Name = "Password")]
        [Compare("ConfirmPassword", ErrorMessage = "Password does not match.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Enter Password again")]
        [Display(Name = "Confirm Password.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
