using System.ComponentModel.DataAnnotations;

namespace Stockmantras.Models
{
    public class News
    {
        [Key]
        public int N_ID { get; set; }

        public string N_HEADING { get; set; }

        public string N_BRIEF { get; set; }

        public string N_IMG_URL { get; set; }

        public string status { get; set; }

    }
}
