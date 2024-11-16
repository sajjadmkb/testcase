using TestService.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestService.Application.Interfaces
{
    public interface IFinancialInstrumentService
    {
        Task<List<FinancialInstrument>> GetAvailableInstrumentsAsync();
        Task<Price> GetCurrentPriceAsync(string symbol);
    }
}
