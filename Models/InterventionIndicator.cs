using BCV_WSCRAP_API.Services;

namespace BCV_WSCRAP_API.Models
{
    public class InterventionIndicator
    {
        public DateTime IndicatorDate { get; set; }

        public string Bank { get; set; }

        public string BankCode { get; set; }

        public decimal Buy { get; set; }

        public decimal Sell { get; set; }

        public void AssignBankCode(IBankDictionary bankDictionary)
        {
            if(bankDictionary.BankCodes[Bank] != null)
                BankCode = bankDictionary.BankCodes[Bank];
        }
    }
}
