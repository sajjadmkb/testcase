namespace TestService.Domain.Models
{
    public class PriceUpdate
    {
        public string Symbol { get; private set; }
        public decimal Price { get; private set; }
        public DateTime Timestamp { get; private set; }

        public PriceUpdate(string symbol, decimal price)
        {
            Symbol = symbol;
            Price = price;
            Timestamp = DateTime.UtcNow;
        }
    }
}
