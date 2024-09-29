using PuppeteerSharp;

namespace BCV_WSCRAP_API.Services
{
    /// <summary>Generic Scrapper that uses PuppeteerSharp</summary>
    public class Scrapper : IScrapper
    {
        private readonly LaunchOptions _launchOptions;

        public Scrapper()
        {
            _launchOptions = new()
            {
                Headless = true,
                Args = ["--no-sandbox",
                    "--disable-setuid-sandbox"]
            };
        }

        /// <summary>Executes Js on page to return the specified object</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">page to use the script</param>
        /// <param name="script">Complete js Script</param>
        /// <returns>Specified object</returns>
        public async Task<T?> GetResultOfScript<T>(string url, string script)
        {
            try
            {
                if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(script))
                    return default;

                await using var browser = await Puppeteer.LaunchAsync(_launchOptions);
                await using var page = await browser.NewPageAsync();

                var response = await page.GoToAsync(url, waitUntil: WaitUntilNavigation.DOMContentLoaded);
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

                await using var browser = await Puppeteer.LaunchAsync(_launchOptions);
                await using var page = await browser.NewPageAsync();
                var firstResponse = await page.GoToAsync(url, waitUntil: WaitUntilNavigation.DOMContentLoaded);
                var newUrl = await page.EvaluateFunctionAsync(script);

                if (newUrl is null)
                    return default;

                var query = newUrl.ToObject<string>();

                var secondResponse = await page.GoToAsync(url + "?"  + query);
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
