using System.ComponentModel.DataAnnotations;

namespace aryamoneygrow.Models
{
    public class MarketOutlook
    {
        [Key]
        public int market_id { get; set; }
        public string brief1 { get; set; }
        public string brief2 { get; set; }
        public string brief3 { get; set; }
    }
}
