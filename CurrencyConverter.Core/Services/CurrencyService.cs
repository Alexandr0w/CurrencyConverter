using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace CurrencyConverter.Services
{
    public class CurrencyService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private readonly ILogger<CurrencyService> _logger;

        private const string Url = "https://open.er-api.com/v6/latest/";

        public CurrencyService(HttpClient httpClient, IMemoryCache cache, ILogger<CurrencyService> logger)
        {
            this._httpClient = httpClient;
            this._cache = cache;
            this._logger = logger;
        }

        public async Task<decimal> GetRateAsync(string from, string to)
        {
            string cacheKey = $"Rate_{from.ToUpper()}";

            if (!this._cache.TryGetValue(cacheKey, out Dictionary<string, decimal>? rates))
            {
                this._logger.LogInformation("Cache miss for {Currency}. Fetching rates from external API...", from.ToUpper());

                try
                {
                    ExternalApiResponse? data = await this._httpClient
                        .GetFromJsonAsync<ExternalApiResponse>($"{Url}{from.ToUpper()}");
                    
                    if (data != null)
                    {
                        rates = data.Rates;

                        MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions()
                            .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));

                        this._cache.Set(cacheKey, rates, cacheOptions);
                        this._logger.LogInformation("Successfully cached rates for {Currency}.", from.ToUpper());
                    }
                }
                catch (Exception ex)
                {
                    this._logger.LogError(ex, "Failed to fetch exchange rates for {Currency} due to an error.", from.ToUpper());
                    return 0;
                }
            }
            else
            {
                this._logger.LogInformation("Cache hit for {Currency}. Returning data from memory cache.", from.ToUpper());
            }

            if (rates != null && rates.ContainsKey(to.ToUpper()))
            {
                return rates[to.ToUpper()];
            }

            this._logger.LogWarning("Exchange rate from {From} to {To} was not found in the data.", from.ToUpper(), to.ToUpper());
            return 0;
        }
    }

    public class ExternalApiResponse
    {
        public Dictionary<string, decimal> Rates { get; set; } = null!;
    }
}
