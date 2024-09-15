namespace BCV_WSCRAP_API.Models
{
    /// <summary>Contains relevant info about Currencies</summary>
    public class Currency
    {
        public string? Name { get; set; }

        /// <summary>Acronym or Abbreviation of Currency</summary>
        public string? Code { get; set; }

        /// <summary>Current exchange rate to BS</summary>
        public decimal ExchangeRate { get; set; }
    }
}
