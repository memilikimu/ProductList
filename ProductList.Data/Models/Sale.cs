using System.ComponentModel.DataAnnotations;

namespace ProductList.Data.Models
{
    public class Sale
    {
        public int Id { get; set; }

        [Required]
        public string Product { get; set; }

        [Required]
        [Range(1, double.MaxValue)]
        public int Price { get; set; }

        [Required]
        [Range(1, double.MaxValue)]
        public int SellPrice { get; set; }

        [Required]
        [Range(1, 100)]
        public int Amount { get; set; }

        public int SellAmount { get; set; }

        public int Total => Amount * Price;
        public int TotalSold => SellAmount * SellPrice;
    }
}
