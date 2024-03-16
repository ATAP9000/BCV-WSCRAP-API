namespace BCV_WSCRAP_API.Utilities
{
    public class CustomHttpClient : HttpClient
    {
        public string? LastIUrlCall { get; set; }

        public CustomHttpClient()
        {

        }

        public CustomHttpClient(HttpMessageHandler handler) : base(handler)
        {

        }

        public async Task<Stream> GetStreamAsyncKeepRequestUrl(string? requestUri)
        {
            LastIUrlCall = requestUri;
            return await GetStreamAsync(requestUri);
        }
    }
}
