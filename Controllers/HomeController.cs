using BCV_WSCRAP_API.Models;
using BCV_WSCRAP_API.Services;
using BCV_WSCRAP_API.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Data;

namespace BCV_WSCRAP_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IScrapper _scrapper;
        private readonly IConfiguration _configuration;
        private readonly IDataTableConverter _dataTableConverter;
        private readonly IBankDictionary _bankDictionary;
        private readonly string _BCVBaseUrl;
        private readonly string _BCVBankingInformationRatesUrl;
        private readonly string _BCVExchangeRateInterventionUrl;
        private readonly string _GetCurrentExchangeRateScript;
        private readonly string _GetMostRecentInterventionScript;
        private readonly string _GetBankExchangeRatesScript;
        private readonly string _GetInterventionsScript;
        private readonly int _CacheHours;

        public HomeController(IMemoryCache memoryCache, IScrapper scrapper, IConfiguration configuration, IDataTableConverter htmlConverter, IBankDictionary bankDictionary )
        {
            _memoryCache = memoryCache;
            _scrapper = scrapper;
            _configuration = configuration;
            _dataTableConverter = htmlConverter;
            _bankDictionary = bankDictionary;
            _BCVBaseUrl = configuration.GetConnectionString("BCV-Base");
            _BCVBankingInformationRatesUrl = configuration.GetConnectionString("BCV-BankingInformationRates");
            _BCVExchangeRateInterventionUrl = configuration.GetConnectionString("BCV-ExchangeRateIntervention");
            _GetCurrentExchangeRateScript = configuration.GetSection("Scripts")["GetCurrentExchangeRate"];
            _GetMostRecentInterventionScript = configuration.GetSection("Scripts")["GetMostRecentIntervention"];
            _GetBankExchangeRatesScript = configuration.GetSection("Scripts")["GetBankExchangeRates"];
            _GetInterventionsScript = configuration.GetSection("Scripts")["GetInterventions"];
            int.TryParse(configuration["CacheHours"], out _CacheHours);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string script = FileHandler.GetFile(_GetCurrentExchangeRateScript);
            var result = await _scrapper.GetResultOfScript<List<Currency>>(_BCVBaseUrl, script);
            return Ok(result);
        }

        [HttpGet("RecentIntervention")]
        public async Task<IActionResult> RecentIntervention()
        {
            string script = FileHandler.GetFile(_GetMostRecentInterventionScript);
            var result = await _scrapper.GetResultOfScript<Intervention>(_BCVExchangeRateInterventionUrl, script);
            return Ok(result);
        }

        [HttpGet("Interventions")]
        public async Task<IActionResult> Interventions([FromQuery] InterventionQuery? query)
        {
            List<Intervention> interventions;
            interventions = _memoryCache.Get<List<Intervention>>("interventions");

            if (interventions == null)
            {
                string script = FileHandler.GetFile(_GetInterventionsScript);
                string scriptResult = await _scrapper.GetResultOfScript<string>(_BCVExchangeRateInterventionUrl, script);
                DataTable interventionsDT = _dataTableConverter.HtmlToDataTable(scriptResult);
                interventions = _dataTableConverter.DataTableToList<Intervention>(interventionsDT);
                _memoryCache.Set("interventions", interventions, TimeSpan.FromHours(_CacheHours));
            }

            if (query == null || query.InterventionCode == null && query.MininumDate == null && query.MaxinumDate == null)
            {
                string JSON = JsonConvert.SerializeObject(interventions);
                return Ok(JSON);
            }

            List<Intervention> queryResult = [];

            if (query.InterventionCode != null)
            {
                if (interventions.Any(x => x.InterventionNumber == query.InterventionCode))
                    queryResult.AddRange(interventions.Where(x => x.InterventionNumber == query.InterventionCode).ToList());
            }

            if (query.MininumDate != null && query.MaxinumDate == null)
            {
                queryResult = queryResult.Union(interventions.Where(x => x.InterventionDate.Date >= query.MininumDate).ToList()).ToList();
            }
            else if(query.MininumDate == null && query.MaxinumDate != null)
            {
                queryResult = queryResult.Union(interventions.Where(x => x.InterventionDate.Date <= query.MaxinumDate).ToList()).ToList();
            }
            else if(query.MininumDate != null && query.MaxinumDate != null)
            {
                queryResult = queryResult.Union(interventions.Where(x => x.InterventionDate.Date >= query.MininumDate && x.InterventionDate.Date <= query.MaxinumDate).ToList()).ToList();
            }

            string JSONresult = JsonConvert.SerializeObject(queryResult.OrderByDescending(x => x.InterventionDate));
            return Ok(JSONresult);
        }

        [HttpGet("BankRates")]
        public async Task<IActionResult> BankRates([FromQuery] BankRateQuery? query)
        {
            List<InterventionIndicator> bankRates;
            bankRates = _memoryCache.Get<List<InterventionIndicator>>("bankrates");

            if (bankRates == null)
            {
                using (HttpClient client = new())
                {
                    using (var s = client.GetStreamAsync(_BCVBankingInformationRatesUrl))
                    {
                        FileHandler.SaveFile(s.Result, "IntervencionesBancos.html");
                    }
                }

                string htmlString = FileHandler.GetFile("IntervencionesBancos.html");
                DataTable bankratesDT = _dataTableConverter.HtmlToDataTable(htmlString);
                bankRates = _dataTableConverter.DataTableToList<InterventionIndicator>(bankratesDT);
                _memoryCache.Set("bankrates", bankRates, TimeSpan.FromHours(_CacheHours));
            }

            bankRates.ForEach(x => { x.AssignBankCode(_bankDictionary); });

            if (query == null || query.BankCode == null && query.MininumDate == null && query.MaxinumDate == null)
            {
                string JSON = JsonConvert.SerializeObject(bankRates);
                return Ok(JSON);
            }

            List<InterventionIndicator> queryResult = [];

            if (query.BankCode != null)
            {
                if (bankRates.Any(x => x.BankCode == query.BankCode))
                    queryResult.AddRange(bankRates.Where(x => x.BankCode == query.BankCode).ToList());
            }

            if (query.MininumDate != null && query.MaxinumDate == null)
            {
                queryResult = queryResult.Union(bankRates.Where(x => x.IndicatorDate.Date >= query.MininumDate).ToList()).ToList();
            }
            else if (query.MininumDate == null && query.MaxinumDate != null)
            {
                queryResult = queryResult.Union(bankRates.Where(x => x.IndicatorDate.Date <= query.MaxinumDate).ToList()).ToList();
            }
            else if (query.MininumDate != null && query.MaxinumDate != null)
            {
                queryResult = queryResult.Union(bankRates.Where(x => x.IndicatorDate.Date >= query.MininumDate && x.IndicatorDate.Date <= query.MaxinumDate).ToList()).ToList();
            }

            string result = JsonConvert.SerializeObject(queryResult);
            return Ok(result);
        }
    }
}
