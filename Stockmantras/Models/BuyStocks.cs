using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace aryamoneygrow.Models
{
    public class BuyStock
    {
        [Required(ErrorMessage = "Please Enter Buy Price.")]
        public double BuyPrice { get; set; }
        public double previous_value { get; set; }
        public int previous_qty { get; set; }
        public string ScriptName { get; set; }
        [Required(ErrorMessage = "Please Enter Quantity Purchased.")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Please Enter Trade Date.")]
        public DateTime TRADE_DATE { get; set; }
    }

   /* // SellStock model
    public class SellStock
    {
        public double BuyPrice { get; set; }
        public double SellPrice { get; set; }
        public string ScriptName { get; set; }
        public int Quantity { get; set; }
    }*/
}
