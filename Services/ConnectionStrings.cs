namespace BCV_WSCRAP_API.Services
{
    public class ConnectionStrings
    {
        public string BCVBase { get; set; }

        public string BCVBankingInformationRates { get; set; }

        public string BCVExchangeRateIntervention { get; set; }

        public ConnectionStrings(IConfigurationSection configuration)
            => configuration.Bind(this);
    }
}
