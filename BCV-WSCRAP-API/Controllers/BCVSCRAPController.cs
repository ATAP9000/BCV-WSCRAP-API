using BCV_WSCRAP_API.Models;
using BCV_WSCRAP_API.Services;
using BCV_WSCRAP_API.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Data;

namespace BCV_WSCRAP_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BCVSCRAPController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IBCVInvoker _BCVInvoker;
        private readonly BankDictionary _bankDictionary;
        private readonly int _CacheHours;
        private readonly string _interventionsCacheName;
        private readonly string _bankRatesCacheName;
        private readonly string _message;

        public BCVSCRAPController(IMemoryCache memoryCache, IBCVInvoker bCVInvoker, IConfiguration configuration, BankDictionary bankDictionary)
        {
            _memoryCache = memoryCache;
            _BCVInvoker = bCVInvoker;
            _bankDictionary = bankDictionary;
            IConfigurationSection cache = configuration.GetSection("CachingSettings");
            _interventionsCacheName = cache["interventionsKey"]!;
            _bankRatesCacheName = cache["bankratesKey"]!;
            _ = int.TryParse(cache["TimeKept"], out _CacheHours);
            _message = configuration["Message"]!;
        }

        #region [ Api Calls ]
        /// <summary>Gets Response of API</summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
            => Ok(_message);

        /// <summary>Retrieves current exchange rate of the available currencies</summary>
        /// <returns>List of Currencies</returns>
        [HttpGet("CurrentExchangeRate")]
        public async Task<IActionResult> CurrentExchangeRate()
        {
            List<Currency>? result = await _BCVInvoker.GetCurrentExchangeRate();
            return Ok(result);
        }

        /// <summary>Retrieves list of exchange rate of USD to BS</summary>
        /// <returns>List of Currencies</returns>
        [HttpGet("ExchangeRates")]
        public async Task<IActionResult> ExchangeRates([FromQuery] ExchangeRateQuery? query)
        {
            query ??= new ExchangeRateQuery();
            var result = await _BCVInvoker.GetExchangeRates(query.MinimumDate, query.MaximumDate);
            return Ok(result);
        }

        /// <summary>Retrieves the most recent intervention</summary>
        /// <returns>An Intervention</returns>
        [HttpGet("RecentIntervention")]
        public async Task<IActionResult> RecentIntervention()
        {
            Intervention? result = await _BCVInvoker.GetRecentIntervention();
            return Ok(result);
        }

        /// <summary>Retrieves list of interventions between a date range</summary>
        /// <param name="query">Query Params</param>
        /// <returns>List Interventions</returns>
        [HttpGet("Interventions")]
        public async Task<IActionResult> Interventions([FromQuery] InterventionQuery? query)
        {
            List<Intervention>? interventions = await HandleInterventionsCache();
            return Ok(QueryInterventions(interventions, query));
        }

        /// <summary>Retrieves list of informational rates of the banking system between a date range.</summary>
        /// <param name="query">Query Params</param>
        /// <returns>List of Bank rates</returns>
        [HttpGet("BankRates")]
        public async Task<IActionResult> BankRates([FromQuery] BankRateQuery? query)
        {
            List<BankRate>? bankRates = await HandleBankRatesCache();
            return Ok(QueryBankRates(bankRates,query));
        }
        #endregion

        #region [ Cache Handling ]
        private async Task<List<Intervention>> HandleInterventionsCache()
        {
            List<Intervention>? interventions = _memoryCache.Get<List<Intervention>>(_interventionsCacheName);

            if (interventions == null)
            {
                interventions = await _BCVInvoker.GetInterventions();
                _memoryCache.Set(_interventionsCacheName, interventions, TimeSpan.FromHours(_CacheHours));
            }
            return interventions!;
        }

        private async Task<List<BankRate>> HandleBankRatesCache()
        {
            List<BankRate>? bankRates = _memoryCache.Get<List<BankRate>>(_bankRatesCacheName);

            if (bankRates == null)
            {
                bankRates = await _BCVInvoker.GetBankRates(new CustomHttpClient());
                _memoryCache.Set(_bankRatesCacheName, bankRates, TimeSpan.FromHours(_CacheHours));
            }

            bankRates.ForEach(x => { x.AssignBankCode(_bankDictionary); });
            return bankRates!;
        }
        #endregion

        #region [ Private Methods ]
        private static List<Intervention> QueryInterventions(List<Intervention> interventions, InterventionQuery? query)
        {
            IList<Intervention> queryResult = [];

            if (query == null || query.IsEmpty())
                queryResult = interventions;
            else
            {
                if (query.InterventionCode != null)
                {
                    if (interventions.Exists(x => x.InterventionNumber == query.InterventionCode))
                        queryResult = interventions.Where(x => x.InterventionNumber == query.InterventionCode).Union(queryResult).ToList();
                }

                if (query.HasOnlyMinimumDate())
                    queryResult = interventions.Where(x => x.InterventionDate.Date >= query.MinimumDate).Union(queryResult).ToList();
                else if (query.HasOnlyMaximumDate())
                    queryResult = interventions.Where(x => x.InterventionDate.Date <= query.MaximumDate).Union(queryResult).ToList();
                else if (query.HasNoDates())
                    queryResult = interventions.Where(x => x.InterventionDate.Date >= query.MinimumDate && x.InterventionDate.Date <= query.MaximumDate).Union(queryResult).ToList();
            }

            return queryResult.OrderByDescending(x => x.InterventionDate).ToList();
        }

        private static List<BankRate> QueryBankRates(List<BankRate> bankRates, BankRateQuery? query)
        {
            IList<BankRate> queryResult = [];
            if (query == null || query.IsEmpty())
                queryResult = bankRates;
            else
            {
                if (query.BankCode != null)
                {
                    if (bankRates.Exists(x => x.BankCode == query.BankCode))
                        queryResult = bankRates.Where(x => x.BankCode == query.BankCode).Union(queryResult).ToList();
                }

                if (query.HasOnlyMinimumDate())
                    queryResult = bankRates.Where(x => x.IndicatorDate.Date >= query.MinimumDate).Union(queryResult).ToList();
                else if (query.HasOnlyMaximumDate())
                    queryResult = bankRates.Where(x => x.IndicatorDate.Date <= query.MaximumDate).Union(queryResult).ToList();
                else if (query.HasNoDates())
                    queryResult = bankRates.Where(x => x.IndicatorDate.Date >= query.MinimumDate && x.IndicatorDate.Date <= query.MaximumDate).Union(queryResult).ToList();
            }

            return queryResult.OrderByDescending(x => x.IndicatorDate).ToList();
        }
        #endregion
    }
}
