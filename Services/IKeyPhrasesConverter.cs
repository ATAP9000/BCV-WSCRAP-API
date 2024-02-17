namespace BCV_WSCRAP_API.Services
{
    public interface IKeyPhrasesConverter
    {
        public Dictionary<string, string> Phrases { get; init; }

        public string EvaluatePhrase(string phrase);
    }
}
