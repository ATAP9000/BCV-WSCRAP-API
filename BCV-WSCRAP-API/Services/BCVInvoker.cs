using BCV_WSCRAP_API.Models;
using BCV_WSCRAP_API.Utilities;
using System.Data;

namespace BCV_WSCRAP_API.Services
{
    /// <summary>Explicit implementation of Scrapper</summary>
    public class BCVInvoker : IBCVInvoker
    {
        private readonly IScrapper _scrapper;
        private readonly DataTableConverter _dataTableConverter;
        private readonly IConnectionStrings _connectionStrings;
        private readonly Scripts _scripts;
        private const string SCRIPT_PATH = "Scripts";

        public BCVInvoker(IConfiguration configuration, IConnectionStrings connectionStrings, IScrapper scrapper)
        {
            _scrapper = scrapper;
            _dataTableConverter = new(configuration);
            _connectionStrings = connectionStrings;
            _scripts = new(configuration.GetSection(SCRIPT_PATH));
        }

        public async Task<List<Currency>?> GetCurrentExchangeRate()
        {
            string scriptPath = Path.Combine(SCRIPT_PATH, _scripts.GetCurrentExchangeRate!);
            string script = FileHandler.GetFile(scriptPath);
            return await _scrapper.GetResultOfScript<List<Currency>>(_connectionStrings.BCVBase!, script);
        }

        public async Task<Intervention?> GetRecentIntervention()
        {
            string scriptPath = Path.Combine(SCRIPT_PATH, _scripts.GetMostRecentIntervention!);
            string script = FileHandler.GetFile(scriptPath);
            return await _scrapper.GetResultOfScript<Intervention>(_connectionStrings.BCVExchangeRateIntervention!, script);
        }

        public async Task<List<BankRate>> GetBankRates(CustomHttpClient httpClient)
        {
            string htmlString = string.Empty;
            using (httpClient)
            {
                using Stream s = await httpClient.GetStreamAsyncKeepRequestUrl(_connectionStrings.BCVBankingInformationRates!);
                StreamReader reader = new(s);
                htmlString = reader.ReadToEnd();
            }
            DataTable bankratesDT = _dataTableConverter.HtmlToDataTable(htmlString);
            return _dataTableConverter.DataTableToList<BankRate>(bankratesDT);
        }

        public async Task<List<Intervention>?> GetInterventions()
        {
            string scriptPath = Path.Combine(SCRIPT_PATH, _scripts.GetInterventions!);
            string script = FileHandler.GetFile(scriptPath);
            string? scriptResult = await _scrapper.GetResultOfScript<string>(_connectionStrings.BCVExchangeRateIntervention!.ToString(), script);
            DataTable interventionsDT = _dataTableConverter.HtmlToDataTable(scriptResult);
            return _dataTableConverter.DataTableToList<Intervention>(interventionsDT);
        }
    }
}
