using PuppeteerSharp;

namespace BCV_WSCRAP_API.Services
{
    /// <summary>Generic Scrapper that uses PuppeteerSharp</summary>
    public class Scrapper : IScrapper
    {
        private readonly ConnectOptions _connectOptions;
        private readonly LaunchOptions? _launchOptions;
        private readonly string _agentUser;
        private bool _isTest;

        public Scrapper(string agentUser, string browserIpAddress)
        {
            _isTest = string.IsNullOrEmpty(Environment.GetEnvironmentVariable("IsTest"));
            if (_isTest)
            {
                _launchOptions = new()
                {
                    Headless = true,
                    Args = ["--no-sandbox",
                    "--disable-setuid-sandbox"]
                };
            }

            _connectOptions = new()
            {
                BrowserWSEndpoint = $"ws://{browserIpAddress}",
                IgnoreHTTPSErrors = true
            };
            _agentUser = agentUser;
        }

        /// <summary>Executes Js on page to return the specified object</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">page to use the script</param>
        /// <param name="script">Complete js Script</param>
        /// <returns>Specified object</returns>
        public async Task<T?> GetResultOfScript<T>(string url, string script, bool isTest: false)
        {
            if(isTest)
                return (T)Convert.ChangeType(new object(), typeof(T));

            try
            {
                if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(script))
                    return default;

                IBrowser browser;
                if (_isTest)
                    browser = await Puppeteer.LaunchAsync(_launchOptions);
                else
                    browser = await Puppeteer.ConnectAsync(_connectOptions);

                await using var page = await browser.NewPageAsync();
                await page.SetUserAgentAsync(_agentUser);
                var response = await page.GoToAsync(url, new NavigationOptions
                {
                    WaitUntil = [WaitUntilNavigation.DOMContentLoaded],
                    ReferrerPolicy = "origin"
                });
                var result = await page.EvaluateFunctionAsync(script);
                return result == null ? default : result.ToObject<T>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return default;
            }
        }

        /// <summary>Executes Js on page to return of the specified object when a reload of the page is required</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">page to use the script</param>
        /// <param name="script">Complete js Script</param>
        /// <returns>Specified object</returns>
        public async Task<T?> GetResultOfScriptWithReload<T>(string url, string script)
        {
            try
            {
                if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(script))
                    return default;

                IBrowser browser;
                if (_isTest)
                    browser = await Puppeteer.LaunchAsync(_launchOptions);
                else
                    browser = await Puppeteer.ConnectAsync(_connectOptions);

                await using var page = await browser.NewPageAsync();
                var firstResponse = await page.GoToAsync(url, waitUntil: WaitUntilNavigation.DOMContentLoaded);
                var newUrl = await page.EvaluateFunctionAsync(script);

                if (newUrl is null)
                    return default;

                var query = newUrl.ToObject<string>();

                var secondResponse = await page.GoToAsync(url + "?" + query);
                var result = await page.EvaluateFunctionAsync(script);
                return result == null ? default : result.ToObject<T>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return default;
            }
        }
    }
}
