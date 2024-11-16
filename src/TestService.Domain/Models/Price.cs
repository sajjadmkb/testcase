namespace TestService.Domain.Models
{
    public class Price
    {
        public string Symbol { get; private set; }
        public decimal Value { get; private set; }
        public DateTime Timestamp { get; private set; }

        public Price(string symbol, decimal value)
        {
            Symbol = symbol;
            Value = value;
            Timestamp = DateTime.UtcNow;
        }
    }
}
