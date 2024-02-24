namespace BCV_WSCRAP_API.Utilities
{
    public class KeyPhrasesConverter
    {
        public Dictionary<string, string> Phrases { get; init; }

        public KeyPhrasesConverter(IConfiguration configuration)
            => configuration.Bind(this);

        public string EvaluatePhrase(string phrase)
        {
            string result = Phrases[phrase];
            return result;
        }
    }
}
