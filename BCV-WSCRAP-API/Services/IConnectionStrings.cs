namespace BCV_WSCRAP_API.Services
{
    public interface IConnectionStrings
    {
        public string? BCVBase { get; set; }

        public string? BCVExchangeRates { get; set; }

        public string? BCVBankingInformationRates { get; set; }

        public string? BCVExchangeRateIntervention { get; set; }
    }
}
