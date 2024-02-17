using PuppeteerSharp;

namespace BCV_WSCRAP_API.Services
{
    public class Scrapper : IScrapper
    {
        private readonly LaunchOptions _launchOptions;

        public Scrapper(string browserPath)
        {
            _launchOptions = new()
            {
                Headless = true,
                ExecutablePath = browserPath,
            };
        }

        public async Task<T> GetResultOfScript<T>(string url, string script)
        {
            using BrowserFetcher fetcher = new();
            await using var browser = await Puppeteer.LaunchAsync(_launchOptions);
            await using var page = await browser.NewPageAsync();
            await page.GoToAsync(url);

            //validar si pagina pudo obtenerse?

            // validar si no falla el script
            var result = await page.EvaluateFunctionAsync(script);

            // validar si todo bien

            return result.ToObject<T>();
        }

    }
}
