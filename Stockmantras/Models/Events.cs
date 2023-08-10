using System;
using System.ComponentModel.DataAnnotations;

namespace aryamoneygrow.Models
{
    public class Events
    {
        [Key]
        public int E_ID { get; set; }
        [DataType(DataType.Html)]
        public string E_COY { get; set; }
        [DataType(DataType.Html)]
        public string E_BRIEF { get; set; }

        public DateTime E_DATE { get; set; }

        public string E_STATUS { get; set; }
    }
}
