namespace BCV_WSCRAP_API.Services
{
    public interface IScrapper
    {
        Task<T> GetResultOfScript<T>(string url, string script);
    }
}
