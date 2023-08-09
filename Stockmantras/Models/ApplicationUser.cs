using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace aryamoneygrow.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Please Enter First Name")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please Enter Last Name")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }
    }
}
