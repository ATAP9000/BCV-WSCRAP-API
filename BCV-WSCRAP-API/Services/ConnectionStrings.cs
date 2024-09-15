namespace BCV_WSCRAP_API.Services
{
    /// <summary>Contains the Links given in the appsettings</summary>
    public class ConnectionStrings : IConnectionStrings
    {
        public string? BCVBase { get; set; }

        public string? BCVBankingInformationRates { get; set; }

        public string? BCVExchangeRateIntervention { get; set; }

        public ConnectionStrings(IConfigurationSection configuration)
            => configuration.Bind(this);
    }
}
