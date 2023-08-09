using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aryamoneygrow.Models
{
    public class Coin
    {
        [Key]
        public int COIN_ID { get; set; }
        public string COIN_NAME { get; set; }
        public string COIN_DESC { get; set; }
        public string COIN_MINT { get; set; }
        public decimal COIN_PRICE { get; set; }
        public string COIN_PIC_URL { get; set; } // Use IFormFile for the uploaded image
        [NotMapped]
        public IFormFile COIN_IMG_URL { get; set; } // This will store the image URL
        public string COIN_BRIEF { get; set; }
    }
}
