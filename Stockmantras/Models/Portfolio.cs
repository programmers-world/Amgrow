using System;
using System.ComponentModel.DataAnnotations;

namespace aryamoneygrow.Models
{
    public class Portfolio
    {
        /* [Required(ErrorMessage = "Please Enter a valid E-Mail address")]
         [Display(Name = "Portfolio Size")]
         [RegularExpression("([0-9]+)")]
         public int PORT_SIZE { get; set; }
 */     [Key]
        public int TRADE_ID { get; set; }

        public string USER_NAME { get; set; }

        [Required(ErrorMessage = "Can't be Blank")]
        [Display(Name = "Select only from Auto Fill List")]
        public string SCRIPT_NAME { get; set; }
        [Required(ErrorMessage = "Select Date")]
        [DataType(DataType.Date)]
        public  DateTime TRADE_DATE { get; set; }
        [Required(ErrorMessage = "Select the quantity Bought")]
        public int QUANTITY { get; set; }
        [Required(ErrorMessage = "Select Buying Price")]
        public double PRICE { get; set; }
        public double INVESTED_AMOUNT { get; set; }
        public string REMARK { get; set; }
        
    }
}
