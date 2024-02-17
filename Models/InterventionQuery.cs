using System.ComponentModel.DataAnnotations;

namespace BCV_WSCRAP_API.Models
{
    public class InterventionQuery
    {
        public DateTime? MininumDate { get; set; }
        public DateTime? MaxinumDate { get; set;}

        [MaxLength(6)]
        [RegularExpression(@"\d{3}-\d{2}")]
        public string? InterventionCode { get; set; }
    }
}
