﻿using BCV_WSCRAP_API.Models;
using BCV_WSCRAP_API.Utilities;
using PuppeteerSharp;
using System.Data;

namespace BCV_WSCRAP_API.Services
{
    public class Scrapper
    {
        private readonly LaunchOptions _launchOptions;
        private readonly DataTableConverter _dataTableConverter;
        private readonly ConnectionStrings _connectionStrings;
        private readonly Scripts _scripts;
        private readonly string _interventionFile;

        public Scrapper(IConfiguration configuration, ConnectionStrings connectionStrings)
        {
            _launchOptions = new()
            {
                Headless = true,
                ExecutablePath = configuration["BrowserRoute"],
            };
            _dataTableConverter = new(configuration);
            _connectionStrings = connectionStrings;
            _interventionFile = configuration["InterventionFile"]!;
            _scripts = new(configuration.GetSection("Scripts"));
        }

        public async Task<T> GetResultOfScript<T>(string url, string script)
        {
            using BrowserFetcher fetcher = new();
            await using var browser = await Puppeteer.LaunchAsync(_launchOptions);
            await using var page = await browser.NewPageAsync();
            await page.GoToAsync(url);
            var result = await page.EvaluateFunctionAsync(script);
            return result.ToObject<T>()!;
        }

        public async Task<List<Currency>> GetCurrentExchangeRate()
        {
            string script = FileHandler.GetFile(_scripts.GetCurrentExchangeRate);
            return await GetResultOfScript<List<Currency>>(_connectionStrings.BCVBase, script);
        }

        public async Task<Intervention> GetRecentIntervention()
        {
            string script = FileHandler.GetFile(_scripts.GetMostRecentIntervention);
            return await GetResultOfScript<Intervention>(_connectionStrings.BCVExchangeRateIntervention, script);
        }

        public async Task<List<BankRate>> GetBankRates()
        {
            using (HttpClient client = new())
            {
                using Stream s = await client.GetStreamAsync(_connectionStrings.BCVBankingInformationRates);
                FileHandler.SaveFile(s, _interventionFile);
            }

            string htmlString = FileHandler.GetFile(_interventionFile);
            DataTable bankratesDT = _dataTableConverter.HtmlToDataTable(htmlString);
            return _dataTableConverter.DataTableToList<BankRate>(bankratesDT);
        }

        public async Task<List<Intervention>> GetInterventions()
        {
            string script = FileHandler.GetFile(_scripts.GetInterventions);
            string scriptResult = await GetResultOfScript<string>(_connectionStrings.BCVExchangeRateIntervention.ToString(), script);
            DataTable interventionsDT = _dataTableConverter.HtmlToDataTable(scriptResult);
            return _dataTableConverter.DataTableToList<Intervention>(interventionsDT);
        }
    }
}