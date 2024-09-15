namespace BCV_WSCRAP_API.Models
{
    /// <summary>Contains relevant info about Interventions</summary>
    public class Intervention
    {
        public DateTime InterventionDate { get; set; }

        /// <summary>Intervention Number Given by BCV</summary>
        /// <remarks>This number may be repeated on different dates</remarks>
        public string? InterventionNumber { get; set; }

        /// <summary>Exchange rate to BS from EUR</summary>
        public decimal ExchangeRate { get; set; }
    }
}
