using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace BCV_WSCRAP_API.IntegrationTest
{
    public class BCVSCRAPIntergrationTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public BCVSCRAPIntergrationTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/BCVSCRAP")]
        [InlineData("/BCVSCRAP/CurrentExchangeRate")]
        [InlineData("/BCVSCRAP/ExchangeRates")]
        [InlineData("/BCVSCRAP/RecentIntervention")]
        [InlineData("/BCVSCRAP/Interventions")]
        [InlineData("/BCVSCRAP/BankRates")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = _factory.CreateClient();
            string customUserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/122.0.0.0 Safari/537.36";
            
            client.DefaultRequestHeaders.Add("User-Agent", customUserAgent);

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.Should().BeSuccessful();
        }
    }
}
