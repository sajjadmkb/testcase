namespace TestService.Infrastructure.Interfaces
{
    public interface IWebSocketPublisher
    {
        Task PublishPriceUpdateAsync(PriceUpdate priceUpdate);
    }
}
