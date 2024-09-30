namespace BCV_WSCRAP_API.Models
{
    public class ExchangeRate
    {
        public DateTime Date { get; set; }

        /// <summary>Value of Bs into $1</summary>
        public decimal ExchangeValue { get; set; }

        /// <summary>The Index will be formed by dividing the exchange rate of the day by a base value and multiplying it by one hundred.</summary>
        /// <remarks>Since 29-09-2021 the value started to return N/A therefore it will be left with 0 in this case</remarks>
        public decimal InvestmentIndex { get; set; }

        public decimal NewExpressionIndex { get; set; }
    }
}
