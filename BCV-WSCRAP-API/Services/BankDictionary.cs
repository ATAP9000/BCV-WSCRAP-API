namespace BCV_WSCRAP_API.Services
{
    public class BankDictionary
    {
        public Dictionary<string, string> BankCodes { get; init; }

        public BankDictionary(IConfiguration configuration)
        {
            configuration.Bind(this);
        }
    }
}
