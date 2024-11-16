namespace TestService.Domain.Models
{
    public class FinancialInstrument
    {
        public string Symbol { get; private set; }
        public string Name { get; private set; }

        public FinancialInstrument(string symbol, string name)
        {
            Symbol = symbol;
            Name = name;
        }
    }
}
