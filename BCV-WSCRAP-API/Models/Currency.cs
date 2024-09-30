namespace BCV_WSCRAP_API.Models
{
    /// <summary>Contains relevant info about Currencies</summary>
    public class Currency
    {
        public string? Name { get; set; }

        /// <summary>Acronym or Abbreviation of Currency</summary>
        public string? Code { get; set; }

        /// <summary>Current exchange rate to Currency</summary>
        public decimal ExchangeRate { get; set; }

        /// <summary>Date of the Data</summary>
        /// <remarks>In Saturdays-Sundays the date displayed is from the next Monday</remarks>
        public DateTime ValueDate { get; set; }
    }
}
