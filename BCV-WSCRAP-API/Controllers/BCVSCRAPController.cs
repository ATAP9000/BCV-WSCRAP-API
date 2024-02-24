using BCV_WSCRAP_API.Models;
using BCV_WSCRAP_API.Services;
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
        private readonly IConfiguration _configuration;
        private readonly Scrapper _scrapper;
        private readonly BankDictionary _bankDictionary;
        private readonly int _CacheHours;
        private readonly string _interventionsCacheName;
        private readonly string _bankRatesCacheName;

        public BCVSCRAPController(IMemoryCache memoryCache, Scrapper scrapper, IConfiguration configuration, BankDictionary bankDictionary)
        {
            _memoryCache = memoryCache;
            _scrapper = scrapper;
            _configuration = configuration;
            _bankDictionary = bankDictionary;
            _interventionsCacheName = configuration.GetSection("Caches")["interventions"]!;
            _bankRatesCacheName = configuration.GetSection("Caches")["bankrates"]!;
            _ = int.TryParse(configuration["CacheHours"], out _CacheHours);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }

        [HttpGet("CurrentExchangeRate")]
        public async Task<IActionResult> CurrentExchangeRate()
        {
            List<Currency> result = await _scrapper.GetCurrentExchangeRate();
            return Ok(result);
        }

        [HttpGet("RecentIntervention")]
        public async Task<IActionResult> RecentIntervention()
        {
            Intervention result = await _scrapper.GetRecentIntervention();
            return Ok(result);
        }

        [HttpGet("Interventions")]
        public async Task<IActionResult> Interventions([FromQuery] InterventionQuery? query)
        {
            List<Intervention> interventions;
            IList<Intervention> queryResult = [];
            interventions = _memoryCache.Get<List<Intervention>>(_interventionsCacheName);

            if (interventions == null)
            {
                interventions = await _scrapper.GetInterventions();
                _memoryCache.Set(_interventionsCacheName, interventions, TimeSpan.FromHours(_CacheHours));
            }

            if (query?.IsEmpty() ?? true)
                queryResult = interventions;
            else
            {
                if (query.InterventionCode != null)
                {
                    if (interventions.Any(x => x.InterventionNumber == query.InterventionCode))
                        queryResult = interventions.Where(x => x.InterventionNumber == query.InterventionCode).Union(queryResult).ToList();
                }

                if (query.HasOnlyMinimumDate())
                    queryResult = interventions.Where(x => x.InterventionDate.Date >= query.MinimumDate).Union(queryResult).ToList();
                else if (query.HasOnlyMaximumDate())
                    queryResult = interventions.Where(x => x.InterventionDate.Date <= query.MaximumDate).Union(queryResult).ToList();
                else if (query.HasNoDates())
                    queryResult = interventions.Where(x => x.InterventionDate.Date >= query.MinimumDate && x.InterventionDate.Date <= query.MaximumDate).Union(queryResult).ToList();
            }

            return Ok(queryResult.OrderByDescending(x => x.InterventionDate));
        }

        [HttpGet("BankRates")]
        public async Task<IActionResult> BankRates([FromQuery] BankRateQuery? query)
        {
            List<BankRate> bankRates;
            IList<BankRate> queryResult = [];
            bankRates = _memoryCache.Get<List<BankRate>>(_bankRatesCacheName);

            if (bankRates == null)
            {
                bankRates = await _scrapper.GetBankRates();
                _memoryCache.Set(_bankRatesCacheName, bankRates, TimeSpan.FromHours(_CacheHours));
            }

            bankRates.ForEach(x => { x.AssignBankCode(_bankDictionary); });

            if (query == null || query.IsEmpty())
                queryResult = bankRates;
            else
            {
                if (query.BankCode != null)
                {
                    if (bankRates.Any(x => x.BankCode == query.BankCode))
                        queryResult = bankRates.Where(x => x.BankCode == query.BankCode).Union(queryResult).ToList();
                }

                if (query.HasOnlyMinimumDate())
                    queryResult = bankRates.Where(x => x.IndicatorDate.Date >= query.MinimumDate).Union(queryResult).ToList();
                else if (query.HasOnlyMaximumDate())
                    queryResult = bankRates.Where(x => x.IndicatorDate.Date <= query.MaximumDate).Union(queryResult).ToList();
                else if (query.HasNoDates())
                    queryResult = bankRates.Where(x => x.IndicatorDate.Date >= query.MinimumDate && x.IndicatorDate.Date <= query.MaximumDate).Union(queryResult).ToList();
            }

            return Ok(queryResult.OrderByDescending(x => x.IndicatorDate));
        }

    }
}
