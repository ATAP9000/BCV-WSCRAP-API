using BCV_WSCRAP_API.Services;
using Microsoft.Extensions.Configuration;
using PuppeteerSharp;

namespace BCV_WSCRAP_API.Test.Fixtures
{
    public class BCVInvokerFixture : IDisposable
    {

        public IConnectionStrings connectionStrings;
        public IConfiguration configuration;

        public BCVInvokerFixture()
        {
            Console.WriteLine("Setting Up Browser...");
            var task = Task.Run(async () => await new BrowserFetcher().DownloadAsync());
            task.Wait();
            Console.WriteLine("Set Up Complete!");
            configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("Testappsettings.json").Build();
            connectionStrings = new ConnectionStrings(configuration.GetSection("ConnectionStrings"));
        }

        public void Dispose()
        {
        }

    }
}
