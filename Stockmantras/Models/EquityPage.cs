using System.ComponentModel.DataAnnotations;

namespace Stockmantras.Models
{
    public class EquityPage
    {
        [Key]
        [MaxLength(10)]
        public string S_CODE { get; set; }
        [MaxLength(50)]
        public string S_ID { get; set; }
        public string S_NAME { get; set; }
        public string STATUS { get; set; }
        public string S_GROUP { get; set; }
        [MaxLength(5)]
        public string FACE_VALUE { get; set; }

        public string S_DESCRIPTION { get; set; }
        public string S_CHART { get; set; }
    }
}
