using System.ComponentModel.DataAnnotations;

namespace BCV_WSCRAP_API.Models
{
    public class BankRateQuery
    {
        public DateTime? MininumDate { get; set; }
        public DateTime? MaxinumDate { get; set; }

        [MaxLength(4)]
        [RegularExpression(@"\d{4}")]
        public string? BankCode { get; set; }
    }
}
