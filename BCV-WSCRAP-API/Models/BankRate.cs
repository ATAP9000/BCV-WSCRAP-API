using BCV_WSCRAP_API.Services;

namespace BCV_WSCRAP_API.Models
{
    /// <summary>Contains relevant info about Exhchange Rates by Bank</summary>
    public class BankRate
    {
        public DateTime IndicatorDate { get; set; }

        /// <summary>Bank Name</summary>
        public string? Bank { get; set; }

        /// <summary>Four Digit code assigned to the bank by SUDEBAN</summary>
        public string? BankCode { get; set; }

        /// <summary>Amount at which the bank buys U.S. dollars in BS</summary>
        public decimal Buy { get; set; }

        /// <summary>Amount at which the bank sell U.S. dollars in BS</summary>
        public decimal Sell { get; set; }

        /// <summary>Assings bank code based on the name of the bank</summary>
        /// <param name="bankDictionary">Dictionary obtained by the appsettings.json</param>
        public void AssignBankCode(BankDictionary? bankDictionary)
        {
            if (bankDictionary != null && bankDictionary.BankCodes != null && bankDictionary.BankCodes.ContainsKey(Bank!))
                BankCode = bankDictionary.BankCodes[Bank!];
        }
    }
}
