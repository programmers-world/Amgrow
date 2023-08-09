using System.ComponentModel.DataAnnotations;

namespace aryamoneygrow.Models
{
    public class Insurance
    {
        [Key]
        public int INSU_ID { get; set; }

        public string INSU_NAME { get; set; }

        public string INSU_COY { get; set; }

        public string INSU_PLAN_TYPE { get; set; }

        public string INSU_LOGO_URL { get; set; }

        public int RATING { get; set; }

        public string INSU_MORE_URL { get; set; }
    }
}
