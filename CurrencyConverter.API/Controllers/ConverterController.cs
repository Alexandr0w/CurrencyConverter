using CurrencyConverter.Models;
using CurrencyConverter.Services;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConverter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConverterController : ControllerBase
    {
        private readonly CurrencyService _currencyService;

        public ConverterController(CurrencyService currencyService)
        {
            this._currencyService = currencyService;
        }

        [HttpGet("convert")]
        public async Task<IActionResult> ConvertCurrency(string from, string to, decimal amount)
        {
            if (amount <= 0)
            {
                return this.BadRequest("The amount must be a positive number.");
            }

            decimal rate = await this._currencyService.GetRateAsync(from, to);

            if (rate == 0)
            {
                return this.NotFound("Invalid currency or connection problem.");
            }

            ConversionResponse response = new ConversionResponse
            {
                From = from.ToUpper(),
                To = to.ToUpper(),
                Amount = amount,
                Result = amount * rate,
                CalculationTime = DateTime.Now
            };

            return this.Ok(response);
        }
    }
}
