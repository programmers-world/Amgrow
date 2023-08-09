using System.ComponentModel.DataAnnotations;

namespace aryamoneygrow.Models
{
    public class Contact
    {
        [Key]
        public int CONTACT_ID { get; set; }

        [Display(Name = "Mobile No.")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Please Enter your Mobile No.")]
        public string MOBILE_NO { get; set; }

        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Please Enter your Email Address.")]
        [EmailAddress(ErrorMessage = "Email Address is not valid.")]
        public string EMAIL { get; set; }

        [Display(Name = "First Name")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Enter Your First Name")]
        public string F_NAME { get; set; }

        [Display(Name = "Last Name")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Enter Your Last Name")]
        public string L_NAME { get; set; }

        [Display(Name = "Your Query Goes Here : ")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Query can't be blank...")]
        public string Q_MESSAGE { get; set; }
    }

}
