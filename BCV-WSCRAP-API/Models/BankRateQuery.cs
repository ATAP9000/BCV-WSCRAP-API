using System.ComponentModel.DataAnnotations;

namespace BCV_WSCRAP_API.Models
{
    /// <summary>Represents Query string to search Bank Rates</summary>
    public class BankRateQuery
    {
        public DateTime? MinimumDate { get; set; }

        public DateTime? MaximumDate { get; set; }

        /// <summary>Four Digit code assigned to the bank by SUDEBAN</summary>
        [MaxLength(4)]
        [RegularExpression(@"\d{4}")]
        public string? BankCode { get; set; }

        public bool IsEmpty() => string.IsNullOrEmpty(BankCode) && MinimumDate == null && MaximumDate == null;

        public bool HasOnlyMinimumDate() => MinimumDate != null && MaximumDate == null;

        public bool HasOnlyMaximumDate() => MinimumDate == null && MaximumDate != null;

        public bool HasNoDates() => MinimumDate != null && MaximumDate != null;
    }
}
