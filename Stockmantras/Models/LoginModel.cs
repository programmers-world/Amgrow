using System.ComponentModel.DataAnnotations;

namespace Stockmantras.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please Enter your Email Address")]
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Email Address is not valid.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter password.")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "  Remember me for future")]
        public bool RememberMe { get; set; }

    }
}
