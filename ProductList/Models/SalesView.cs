using ProductList.Data.Models;

namespace ProductList.Models
{
    public class SalesView
    {
        public int Id { get; set; }
        public string Product { get; set; }
        public int Amount { get; set; }
        public int Price { get; set; }
        public int SellAmount { get; set; }
        public int SellPrice { get; set; }
        
        public int Total => Amount * Price;
        public int TotalSold => SellAmount * SellPrice;

        public SalesView() { }

        public SalesView(Sale sale)
        {
            Id = sale.Id;
            Product = sale.Product;
            Amount = sale.Amount;
            Price = sale.Price;
            SellAmount = sale.SellAmount;
            SellPrice = sale.SellPrice;
        }
        public List<SalesView> ToList(IEnumerable<Sale> sales)
        {
            List<SalesView> result = new List<SalesView>();
            foreach (Sale sale in sales)
            {
                result.Add(new SalesView(sale));
            }
            return result;
        }

        public Sale ToEntity(Sale sale)
        {
            sale.Product = Product;
            sale.Price = Price;
            sale.Amount = Amount;
            sale.SellAmount = SellAmount;
            sale.SellPrice = SellPrice;
            return sale;
        }
    }
}
