using System.Net.Http.Json;

namespace CurrencyConverter.Services
{
    public class CurrencyService
    {
        private readonly HttpClient _httpClient;
        private const string Url = "https://open.er-api.com/v6/latest/";
        
        public CurrencyService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<decimal> GetRateAsync(string from, string to)
        {
            try
            {
                ExternalApiResponse? data = await this._httpClient.GetFromJsonAsync<ExternalApiResponse>($"{Url}{from.ToUpper()}");
                
                if (data != null && data.Rates.ContainsKey(to.ToUpper()))
                {
                    return data.Rates[to.ToUpper()];
                }
                return 0;
            }
            catch 
            {
                return 0; 
            }
        }
    }

    public class ExternalApiResponse
    {
        public Dictionary<string, decimal> Rates { get; set; } = null!;
    }
}
