namespace BCV_WSCRAP_API.Services
{
    /// <summary>Has information about js file paths</summary>
    public class Scripts
    {
        public string? GetCurrentExchangeRate { get; set; }

        public string? GetMostRecentIntervention { get; set; }

        public string? GetInterventions { get; set; }

        public Scripts(IConfigurationSection configuration)
        {
            configuration.Bind(this);
        }
    }
}
