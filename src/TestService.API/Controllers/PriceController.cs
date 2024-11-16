using Microsoft.AspNetCore.Mvc;
using TestService.Application.Interfaces;
using TestService.Domain.Models;
using System.Threading.Tasks;

namespace TestService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceController : ControllerBase
    {
        private readonly IFinancialInstrumentService _financialInstrumentService;

        public PriceController(IFinancialInstrumentService financialInstrumentService)
        {
            _financialInstrumentService = financialInstrumentService;
        }

        [HttpGet("instruments")]
        public async Task<IActionResult> GetAvailableInstruments()
        {
            var instruments = await _financialInstrumentService.GetAvailableInstrumentsAsync();
            return Ok(instruments);
        }

        [HttpGet("price/{symbol}")]
        public async Task<IActionResult> GetPrice(string symbol)
        {
            var price = await _financialInstrumentService.GetCurrentPriceAsync(symbol);
            return Ok(price);
        }
    }
}
