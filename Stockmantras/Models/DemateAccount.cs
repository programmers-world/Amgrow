using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stockmantras.Models
{
    public class DemateAccount
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter your Full Name")]
        [Display(Name = "Full Name")]
        [DataType(DataType.Text)]
        public string CUST_NAME { get; set; }

        [Required(ErrorMessage = "Please Enter Your Mobile No.")]
        [Display(Name = "Mobile No")]
        [DataType(DataType.PhoneNumber)]
        public string CONTACT_NO { get; set; }

        [Required(ErrorMessage = "Please Enter Your Email-ID.")]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string EMAIL_ID { get; set; }
    }
}
