using System.ComponentModel.DataAnnotations;

namespace BCV_WSCRAP_API.Models
{
    public class BankRateQuery
    {
        public DateTime? MinimumDate { get; set; }
        public DateTime? MaximumDate { get; set; }

        [MaxLength(4)]
        [RegularExpression(@"\d{4}")]
        public string? BankCode { get; set; }

        public bool IsEmpty() => BankCode == null && MinimumDate == null && MaximumDate == null;

        public bool HasOnlyMinimumDate() => MinimumDate != null && MaximumDate == null;

        public bool HasOnlyMaximumDate() => MinimumDate != null && MaximumDate == null;

        public bool HasNoDates() => MinimumDate != null && MaximumDate != null;
    }
}
