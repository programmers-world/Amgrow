using System;
using System.ComponentModel.DataAnnotations;

namespace Stockmantras.Models
{
    public class Transaction
    {
        [Key]
        [MaxLength(10)]
        public string T_ID { get; set; }
        public string ORDER_TYPE { get; set; }
        public string EXCHANGE { get; set; }
        [Required]
        public string SCRIPT_CODE { get; set; }
        public DateTime TRADE_DATE { get; set; }
        [MaxLength(8)]
        public int QUANTITY { get; set; }
        [MaxLength(10)]
        public float PRICE { get; set; }
        [MaxLength(150)]
        public string REMARKS { get; set; }
    }
}
