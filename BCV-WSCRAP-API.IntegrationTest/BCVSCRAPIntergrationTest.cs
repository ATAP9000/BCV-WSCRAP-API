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

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

    }
}
