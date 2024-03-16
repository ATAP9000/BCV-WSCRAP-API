using BCV_WSCRAP_API.Services;
using BCV_WSCRAP_API.Test.Fixtures;
using BCV_WSCRAP_API.Utilities;
using FluentAssertions;
using Moq;
using Moq.Protected;

namespace BCV_WSCRAP_API.Test.ServicesTests
{
    public class BCVInvokerTest : IClassFixture<BCVInvokerFixture>
    {
        private readonly BCVInvokerFixture Fixture;

        public BCVInvokerTest(BCVInvokerFixture fixture)
        {
            Fixture = fixture;
        }

        #region [GetCurrentExchangeRate Method]
        [Fact]
        public async Task GetCurrentExchangeRate_ExistingUrlAndScript_ReturnsObject()
        {
            //Arrange
            FakeScrapper scrapper = new();
            BCVInvoker bcv = new(Fixture.configuration, Fixture.connectionStrings, scrapper);
            string EXPECTED_SCRIPT = "() => { return \"GetCurrentExchangeRate.js\" }";

            //Act
            await bcv.GetCurrentExchangeRate();

            //Assert
            scrapper.ReceivedUrl.Should().Be(Fixture.connectionStrings.BCVBase);
            scrapper.ReceivedScript.Replace(Environment.NewLine, string.Empty).Should().Be(EXPECTED_SCRIPT);
        }
        #endregion

        #region [GetRecentIntervention Method]
        [Fact]
        public async Task GetRecentIntervention_ExistingUrlAndScript_ReturnsObject()
        {
            //Arrange
            FakeScrapper scrapper = new();
            BCVInvoker bcv = new(Fixture.configuration, Fixture.connectionStrings, scrapper);
            string EXPECTED_SCRIPT = "() => { return \"GetMostRecentIntervention.js\" }";

            //Act
            await bcv.GetRecentIntervention();

            //Assert
            scrapper.ReceivedUrl.Should().Be(Fixture.connectionStrings.BCVExchangeRateIntervention);
            scrapper.ReceivedScript.Replace(Environment.NewLine, string.Empty).Should().Be(EXPECTED_SCRIPT);
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
            BCVInvoker bcv = new(Fixture.configuration, Fixture.connectionStrings, scrapper);

            //Act
            await bcv.GetBankRates(httpClient);

            //Assert
            httpClient.LastIUrlCall.Should().Be(Fixture.connectionStrings.BCVBankingInformationRates);
        }
        #endregion

        #region [GetInterventions Method]
        [Fact]
        public async Task GetInterventions_ExistingUrlAndScript_ReturnsObject()
        {
            try
            {


            //Arrange
            FakeScrapper scrapper = new();
            BCVInvoker bcv = new(Fixture.configuration, Fixture.connectionStrings, scrapper);
            string EXPECTED_SCRIPT = "() => { return \"GetInterventions.js\" }";

            //Act
            await bcv.GetInterventions();

            //Assert
            scrapper.ReceivedUrl.Should().Be(Fixture.connectionStrings.BCVExchangeRateIntervention);
            scrapper.ReceivedScript.Replace(Environment.NewLine, string.Empty).Should().Be(EXPECTED_SCRIPT);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion
    }
}
