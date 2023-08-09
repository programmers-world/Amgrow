using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace aryamoneygrow.Models
{
    public class Nifty50Stocks
    {
        [Key]
        public string SYMBOL { get; set; }
        public string STOCK_NAME { get; set; }
        public double OPEN_PRICE { get; set; }
        public double HIGH { get; set; }
        public double LOW { get; set; }
        public double PREV_CLOSE { get; set; }
        public double LTP { get; set; }
    }
}
