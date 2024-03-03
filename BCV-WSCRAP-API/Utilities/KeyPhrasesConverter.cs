namespace BCV_WSCRAP_API.Utilities
{
    public class KeyPhrasesConverter
    {
        public Dictionary<string, string>? Phrases { get; init; }

        public KeyPhrasesConverter(IConfiguration configuration)
        {
            configuration.Bind(this);
        }

        public string EvaluatePhrase(string? phrase, bool convertHeader = true)
        {
            if (string.IsNullOrEmpty(phrase) || Phrases == null)
                return "";
            else if (!Phrases.ContainsKey(phrase) && convertHeader)
                return Phrases[phrase];
            else
                return phrase;
        }
    }
}
