using System.ComponentModel.DataAnnotations;

namespace aryamoneygrow.Models
{
    public class Equities
    {
        [Key]
        public string SYMBOL { get; set; }
        public string S_NAME { get; set; }
        public double OPEN_PRICE { get; set; }
        public double HIGH_PRICE { get; set; }
        public double LOW_PRICE { get; set; }
        public double PREV_CLOSE { get; set; }
        public double LTP { get; set; }
        public string INDUSTRY { get; set; }
        public string REMARK { get; set; }
    }

}
