namespace BCV_WSCRAP_API.Models
{
    public class ExchangeRateQuery
    {
        public DateTime? MinimumDate { get; set; }

        public DateTime? MaximumDate { get; set; }

        public bool HasOnlyMinimumDate() => MinimumDate != null && MaximumDate == null;

        public bool HasOnlyMaximumDate() => MinimumDate == null && MaximumDate != null;

        public bool HasNoDates() => MinimumDate == null && MaximumDate == null;
    }
}
