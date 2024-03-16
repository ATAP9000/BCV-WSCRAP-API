using BCV_WSCRAP_API.Controllers;
using BCV_WSCRAP_API.Models;
using BCV_WSCRAP_API.Services;
using BCV_WSCRAP_API.Utilities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Moq;

namespace BCV_WSCRAP_API.Test.ControllersTest
{
    public class BCVSCRAPControllerTest
    {
        private readonly IConfiguration _configuration;

        public BCVSCRAPControllerTest()
        {
            string APPSETTINGS_FILE = "Testappsettings.json";
            _configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(APPSETTINGS_FILE).Build();
        }

        #region [Index]

        [Fact]
        public async Task Index_GetAction_ReturnsMessage()
        {
            // Arrange
            var memoryCacheMock = new Mock<IMemoryCache>();
            var bCVInvokerMock = new Mock<IBCVInvoker>();
            var dictionaryMock = new Mock<BankDictionary>(_configuration);
            BCVSCRAPController controller = new(memoryCacheMock.Object, bCVInvokerMock.Object, _configuration, dictionaryMock.Object);

            // Act
            var result = controller.Index() as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
            result.Value.Should().NotBeNull();
            result.Value.Should().BeOfType(typeof(string));
        }

        #endregion

        #region [CurrentExchangeRate]

        [Fact]
        public async Task CurrentExchangeRate_GetAction_ReturnsListOfCurrencies()
        {
            // Arrange
            var memoryCacheMock = new Mock<IMemoryCache>();
            var bCVInvokerMock = new Mock<IBCVInvoker>();
            var dictionaryMock = new Mock<BankDictionary>(_configuration);
            bCVInvokerMock.Setup(e => e.GetCurrentExchangeRate())
                          .ReturnsAsync(new List<Currency> { new Currency { Code = "1", CurrentRate = 1.1M, Name = "Test" } });

            BCVSCRAPController controller = new(memoryCacheMock.Object, bCVInvokerMock.Object, _configuration, dictionaryMock.Object);

            // Act
            var result = await controller.CurrentExchangeRate() as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
            result.Value.Should().NotBeNull();
            result.Value.Should().BeOfType(typeof(List<Currency>));
            result.Value.As<List<Currency>>().Should().NotBeNull();
            result.Value.As<List<Currency>>().Should().HaveCountGreaterThan(0);
        }

        #endregion

        #region [RecentIntervention]

        [Fact]
        public async Task RecentIntervention_GetAction_ReturnsIntervention()
        {
            // Arrange
            var memoryCacheMock = new Mock<IMemoryCache>();
            var bCVInvokerMock = new Mock<IBCVInvoker>();
            var dictionaryMock = new Mock<BankDictionary>(_configuration);
            bCVInvokerMock.Setup(e => e.GetRecentIntervention())
                          .ReturnsAsync(new Intervention { ExchangeRate = 1.1M, InterventionDate = DateTime.Now, InterventionNumber = "1" } );

            BCVSCRAPController controller = new(memoryCacheMock.Object, bCVInvokerMock.Object, _configuration, dictionaryMock.Object);

            // Act
            var result = await controller.RecentIntervention() as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
            result.Value.Should().NotBeNull();
            result.Value.Should().BeOfType(typeof(Intervention));
            result.Value.As<Intervention>().Should().NotBeNull();
        }

        #endregion

        #region [Interventions]

        [Fact]
        public async Task Interventions_GetActionWithoutQuery_ReturnsListOfInterventions()
        {
            // Arrange
            var bCVInvokerMock = new Mock<IBCVInvoker>();
            var dictionaryMock = new Mock<BankDictionary>(_configuration);
            bCVInvokerMock.Setup(e => e.GetInterventions())
                          .ReturnsAsync(new List<Intervention> { new Intervention { ExchangeRate = 1.1M, InterventionDate = DateTime.Now, InterventionNumber = "1" } });
            var memoryCache = SetUpMemoryCacheMock();
            BCVSCRAPController controller = new(memoryCache.Object, bCVInvokerMock.Object, _configuration, dictionaryMock.Object);

            // Act
            var result = await controller.Interventions(null) as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
            result.Value.Should().NotBeNull();
            result.Value.Should().BeOfType(typeof(List<Intervention>));
            result.Value.As<List<Intervention>>().Should().NotBeNull();
            result.Value.As<List<Intervention>>().Should().HaveCountGreaterThan(0);
        }

        [Fact]
        public async Task Interventions_GetActionWithQuery_ReturnsListOfInterventions()
        {
            // Arrange
            var bCVInvokerMock = new Mock<IBCVInvoker>();
            var dictionaryMock = new Mock<BankDictionary>(_configuration);
            bCVInvokerMock.Setup(e => e.GetInterventions())
                          .ReturnsAsync(new List<Intervention> { new Intervention { ExchangeRate = 1.1M, InterventionDate = DateTime.Now, InterventionNumber = "000-00" } });
            var memoryCache = SetUpMemoryCacheMock();
            var query = new InterventionQuery() { InterventionCode = "000-00" };

            BCVSCRAPController controller = new(memoryCache.Object, bCVInvokerMock.Object, _configuration, dictionaryMock.Object);

            // Act
            var result = await controller.Interventions(query) as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
            result.Value.Should().NotBeNull();
            result.Value.Should().BeOfType(typeof(List<Intervention>));
            result.Value.As<List<Intervention>>().Should().NotBeNull();
            result.Value.As<List<Intervention>>().Should().HaveCountGreaterThan(0).And.HaveCountLessThan(2);
        }

        #endregion

        #region [ BankRates ]

        [Fact]
        public async Task BankRates_GetActionWithQuery_ReturnsListOfBankRates()
        {
            // Arrange
            var bCVInvokerMock = new Mock<IBCVInvoker>();
            var dictionaryMock = new Mock<BankDictionary>(_configuration);
            bCVInvokerMock.Setup(e => e.GetBankRates(It.IsAny<CustomHttpClient>()))
                          .ReturnsAsync(new List<BankRate> { new BankRate { BankCode = "1", Bank = "aaa", Buy = 1.1M, IndicatorDate = DateTime.Now, Sell = 1.1M } });

            var memoryCache = SetUpMemoryCacheMock();
            BCVSCRAPController controller = new(memoryCache.Object, bCVInvokerMock.Object, _configuration, dictionaryMock.Object);

            // Act
            var result = await controller.BankRates(null) as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
            result.Value.Should().NotBeNull();
            result.Value.Should().BeOfType(typeof(List<BankRate>));
            result.Value.As<List<BankRate>>().Should().NotBeNull();
            result.Value.As<List<BankRate>>().Should().HaveCountGreaterThan(0);
        }

        [Fact]
        public async Task BankRates_GetActionWithoutQuery_ReturnsListOfBankRates()
        {
            // Arrange
            var bCVInvokerMock = new Mock<IBCVInvoker>();
            var dictionaryMock = new Mock<BankDictionary>(_configuration);
            bCVInvokerMock.Setup(e => e.GetBankRates(It.IsAny<CustomHttpClient>()))
                          .ReturnsAsync(new List<BankRate> { new BankRate { BankCode = "1111", Bank = "aaa", Buy = 1.1M, IndicatorDate = DateTime.Now, Sell = 1.1M } });

            var memoryCache = SetUpMemoryCacheMock();
            var query = new BankRateQuery() { BankCode = "1111" };

            BCVSCRAPController controller = new(memoryCache.Object, bCVInvokerMock.Object, _configuration, dictionaryMock.Object);

            // Act
            var result = await controller.BankRates(query) as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
            result.Value.Should().NotBeNull();
            result.Value.Should().BeOfType(typeof(List<BankRate>));
            result.Value.As<List<BankRate>>().Should().NotBeNull();
            result.Value.As<List<BankRate>>().Should().HaveCountGreaterThan(0);
        }

        #endregion

        private Mock<IMemoryCache> SetUpMemoryCacheMock()
        {
            var mockCache = new Mock<IMemoryCache>();
            var mockCacheEntry = new Mock<ICacheEntry>();

            string? keyPayload = null;
            mockCache.Setup(mc => mc.CreateEntry(It.IsAny<object>()))
                     .Callback((object k) => keyPayload = (string)k)
                     .Returns(mockCacheEntry.Object); // this should address null reference exception

            object? valuePayload = null;
            mockCacheEntry.SetupSet(mce => mce.Value = It.IsAny<object>())
                          .Callback<object>(v => valuePayload = v);

            TimeSpan? expirationPayload = null;
            mockCacheEntry.SetupSet(mce => mce.AbsoluteExpirationRelativeToNow = It.IsAny<TimeSpan?>())
                          .Callback<TimeSpan?>(dto => expirationPayload = dto);

            return mockCache;
        }

    }
}
