namespace BCV_WSCRAP_API.Services
{
    /// <summary>Dictionary with bank names</summary>
    public class BankDictionary
    {
        public Dictionary<string, string> BankCodes { get; init; }

        public BankDictionary(IConfiguration configuration)
        {
            configuration.Bind(this);
            BankCodes ??= [];
        }
    }
}
