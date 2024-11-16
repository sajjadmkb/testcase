using TestService.Application.Interfaces;
using TestService.Domain.Models;
using TestService.Infrastructure.Interfaces;
using System.Threading.Tasks;

namespace TestService.Application.Services
{
    public class PriceUpdateService : IFinancialInstrumentService
    {
        private readonly IPriceRepository _priceRepository;
        private readonly IWebSocketPublisher _webSocketPublisher;

        public PriceUpdateService(IPriceRepository priceRepository, IWebSocketPublisher webSocketPublisher)
        {
            _priceRepository = priceRepository;
            _webSocketPublisher = webSocketPublisher;
        }

        public async Task<List<FinancialInstrument>> GetAvailableInstrumentsAsync()
        {
            return new List<FinancialInstrument>
            {
                new FinancialInstrument("EURUSD", "Euro to US Dollar"),
                new FinancialInstrument("USDJPY", "US Dollar to Japanese Yen"),
                new FinancialInstrument("BTCUSD", "Bitcoin to US Dollar")
            };
        }

        public async Task<Price> GetCurrentPriceAsync(string symbol)
        {
            return await _priceRepository.GetPriceAsync(symbol);
        }

        public async Task UpdatePriceAsync(string symbol, decimal newPrice)
        {
            var priceUpdate = new PriceUpdate(symbol, newPrice);
            await _priceRepository.SavePriceAsync(priceUpdate);
            await _webSocketPublisher.PublishPriceUpdateAsync(priceUpdate);
        }
    }
}
