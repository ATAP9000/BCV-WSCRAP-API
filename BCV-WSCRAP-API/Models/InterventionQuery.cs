using System.ComponentModel.DataAnnotations;

namespace BCV_WSCRAP_API.Models
{
    public class InterventionQuery
    {
        public DateTime? MinimumDate { get; set; }
        public DateTime? MaximumDate { get; set;}

        [MaxLength(6)]
        [RegularExpression(@"\d{3}-\d{2}")]
        public string? InterventionCode { get; set; }

        public bool IsEmpty() => InterventionCode == null && MinimumDate == null && MaximumDate == null;

        public bool HasOnlyMinimumDate() => MinimumDate != null && MaximumDate == null;

        public bool HasOnlyMaximumDate() => MinimumDate != null && MaximumDate == null;

        public bool HasNoDates() => MinimumDate != null && MaximumDate != null;

        public static bool IsNullOrEmpty(InterventionQuery query)
        {
            return query == null || query.IsEmpty();
        }

    }
}
