using BCV_WSCRAP_API.Services;
using BCV_WSCRAP_API.Utilities;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using PuppeteerSharp;

namespace BCV_WSCRAP_API.Test.ServicesTests
{
    public class BCVInvokerTest
    {
        public IConnectionStrings connectionStrings;
        public IConfiguration configuration;

        public BCVInvokerTest()
        {
            Console.WriteLine("Setting Up Browser...");
            var browserList  = new BrowserFetcher().GetInstalledBrowsers();
            if(browserList == null || browserList.Count() < 1)
            {
                var task = Task.Run(async () => await new BrowserFetcher().DownloadAsync());
                task.Wait();
            }
            Console.WriteLine("Set Up Complete!");
            configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("Testappsettings.json").Build();
            connectionStrings = new ConnectionStrings(configuration.GetSection("ConnectionStrings"));
        }

        #region [GetCurrentExchangeRate Method]
        [Fact]
        public async Task GetCurrentExchangeRate_ExistingUrlAndScript_ReturnsObject()
        {
            //Arrange
            FakeScrapper scrapper = new();
            BCVInvoker bcv = new(configuration, connectionStrings, scrapper);
            string EXPECTED_SCRIPT = "() => { return \"GetCurrentExchangeRate.js\" }";

            //Act
            await bcv.GetCurrentExchangeRate();

            //Assert
            scrapper.ReceivedUrl.Should().Be(connectionStrings.BCVBase);
            scrapper.ReceivedScript!.Replace(Environment.NewLine, string.Empty).Should().Be(EXPECTED_SCRIPT);
        }
        #endregion

        #region [GetRecentIntervention Method]
        [Fact]
        public async Task GetRecentIntervention_ExistingUrlAndScript_ReturnsObject()
        {
            //Arrange
            FakeScrapper scrapper = new();
            BCVInvoker bcv = new(configuration, connectionStrings, scrapper);
            string EXPECTED_SCRIPT = "() => { return \"GetMostRecentIntervention.js\" }";

            //Act
            await bcv.GetRecentIntervention();

            //Assert
            scrapper.ReceivedUrl.Should().Be(connectionStrings.BCVExchangeRateIntervention);
            scrapper.ReceivedScript!.Replace(Environment.NewLine, string.Empty).Should().Be(EXPECTED_SCRIPT);
        }
        #endregion

        #region [GetBankRates Method]
        [Fact]
        public async Task GetBankRates_ExistingUrlAndScript_ReturnsObject()
        {
            //Arrange
            FakeScrapper scrapper = new();
            var messageHandler = new Mock<HttpMessageHandler>();
            messageHandler.Protected()
                           .Setup<Task<HttpResponseMessage>>("SendAsync",
                                                             ItExpr.IsAny<HttpRequestMessage>(),
                                                             ItExpr.IsAny<CancellationToken>())
                           .ReturnsAsync((HttpRequestMessage request, CancellationToken token) =>
                           {
                               HttpResponseMessage response = new();
                               return response;
                           });

            CustomHttpClient httpClient = new(messageHandler.Object);
            BCVInvoker bcv = new(configuration, connectionStrings, scrapper);

            //Act
            await bcv.GetBankRates(httpClient);

            //Assert
            httpClient.LastIUrlCall.Should().Be(connectionStrings.BCVBankingInformationRates);
        }
        #endregion

        #region [GetInterventions Method]
        [Fact]
        public async Task GetInterventions_ExistingUrlAndScript_ReturnsObject()
        {
            //Arrange
            FakeScrapper scrapper = new();
            BCVInvoker bcv = new(configuration, connectionStrings, scrapper);
            string EXPECTED_SCRIPT = "() => { return \"GetInterventions.js\" }";

            //Act
            await bcv.GetInterventions();

            //Assert
            scrapper.ReceivedUrl.Should().Be(connectionStrings.BCVExchangeRateIntervention);
            scrapper.ReceivedScript!.Replace(Environment.NewLine, string.Empty).Should().Be(EXPECTED_SCRIPT);
        }
        #endregion
    }
}
