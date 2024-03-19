using PuppeteerSharp;

namespace BCV_WSCRAP_API.Test
{
    public class PuppeteerFixture : IDisposable
    {
        public PuppeteerFixture()
        {
            Console.WriteLine("Setting Up Browser...");
            var browserList = new BrowserFetcher().GetInstalledBrowsers();
            if (browserList == null || browserList.Count() < 1)
            {
                var task = Task.Run(async () => await new BrowserFetcher().DownloadAsync());
                task.Wait();
            }
            Console.WriteLine("Set Up Complete!");
        }

        public void Dispose()
        {

        }
    }

    [CollectionDefinition("Puppeteer collection")]
    public class PuppeteerCollection : ICollectionFixture<PuppeteerFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
