using BCV_WSCRAP_API.Models;
using BCV_WSCRAP_API.Utilities;

namespace BCV_WSCRAP_API.Services
{
    public interface IBCVInvoker
    {
        public Task<List<Currency>?> GetCurrentExchangeRate();

        public Task<List<ExchangeRate>?> GetExchangeRates(DateTime? minimumDate, DateTime? maximumDate);

        public Task<Intervention?> GetRecentIntervention();

        public Task<List<BankRate>> GetBankRates(CustomHttpClient httpClient);

        public Task<List<Intervention>?> GetInterventions();
    }
}
