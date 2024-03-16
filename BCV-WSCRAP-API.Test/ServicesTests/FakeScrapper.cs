using BCV_WSCRAP_API.Services;

namespace BCV_WSCRAP_API.Test.ServicesTests
{
    public class FakeScrapper : IScrapper
    {

        public string ReceivedUrl { get; set; }

        public string ReceivedScript { get; set; }

        public async Task<T?> GetResultOfScript<T>(string url, string script)
        {
            ReceivedUrl = url;

            ReceivedScript = script;

            if (typeof(T) == typeof(string))
                return (T?)Activator.CreateInstance(typeof(string), "a".ToCharArray());
            else
                return Activator.CreateInstance<T>();
        }
    }
}
