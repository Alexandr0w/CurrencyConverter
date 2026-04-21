namespace CurrencyConverter.Models
{
    public class ConversionResponse
    {
        public string From { get; set; } = null!;
        public string To { get; set; } = null!;
        public decimal Amount { get; set; }
        public decimal Result { get; set; }
        public DateTime CalculationTime { get; set; }
    }
}
