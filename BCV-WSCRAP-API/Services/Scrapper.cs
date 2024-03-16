using PuppeteerSharp;

namespace BCV_WSCRAP_API.Services
{
    public class Scrapper : IScrapper
    {
        private readonly LaunchOptions _launchOptions;

        public Scrapper()
        {
            _launchOptions = new()
            {
                Headless = true,
            };
        }

        public async Task<T?> GetResultOfScript<T>(string url, string script)
        {
            try
            {
                if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(script))
                    return default;

                await using var browser = await Puppeteer.LaunchAsync(_launchOptions);
                await using var page = await browser.NewPageAsync();

                var response = await page.GoToAsync(url);
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
