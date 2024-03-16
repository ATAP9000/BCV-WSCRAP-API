namespace BCV_WSCRAP_API.Services
{
    public interface IScrapper
    {
        public Task<T?> GetResultOfScript<T>(string url, string script);
    }
}
