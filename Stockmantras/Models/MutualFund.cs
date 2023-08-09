using System.ComponentModel.DataAnnotations;

namespace aryamoneygrow.Models
{
    public class MutualFund
    {
        [Key]
        public int FUND_ID { get; set; }

        public string FUND_NAME { get; set; }

        public string FUND_DESC { get; set; }

        public int RATING { get; set; }

        public string FUND_LOGO_URL { get; set; }

        public string FUND_MORE_URL { get; set; }


    }
}
