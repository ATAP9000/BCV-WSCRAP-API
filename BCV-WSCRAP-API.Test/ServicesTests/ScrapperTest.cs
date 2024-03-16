using BCV_WSCRAP_API.Services;
using BCV_WSCRAP_API.Test.Fixtures;
using FluentAssertions;

namespace BCV_WSCRAP_API.Test.ServicesTests
{
    public class ScrapperTest : IClassFixture<ScrapperFixture>
    {
        private const string TEST_SCRIPT = "() => { console.log('hi'); }";
        private const string TEST_SCRIPT_WITH_RESULT = "() => { return 1; }";
        private const string TEST_URL = "https://www.google.com/";
        private const string TEST_WRONG_URL = "https://www.google.come/";
        private const string TEST_WRONG_SCRIPT = "console.log('hi')";
        private readonly ScrapperFixture Fixture;

        public ScrapperTest(ScrapperFixture fixture)
        {
            Fixture = fixture;
        }


        #region [GetResultOfScript Method]
        [Fact]
        public async Task GetResultOfScript_NoUrlNoScript_ReturnsNull()
        {
            //Arrange
            Scrapper scrapper = new();
            string url = string.Empty;
            string script = string.Empty;

            //Act
            var result = await scrapper.GetResultOfScript<object>(url, script);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetResultOfScript_NoUrlButScript_ReturnsNull()
        {
            //Arrange
            Scrapper scrapper = new();
            string url = string.Empty;
            string script = TEST_SCRIPT;

            //Act
            var result = await scrapper.GetResultOfScript<object>(url, script);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetResultOfScript_UrlNoScript_ReturnsNull()
        {
            //Arrange
            Scrapper scrapper = new();
            string url = TEST_URL;
            string script = string.Empty;

            //Act
            var result = await scrapper.GetResultOfScript<object>(url, script);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetResultOfScript_NotExistingUrlButScript_ReturnsNull()
        {
            //Arrange
            Scrapper scrapper = new();
            string url = TEST_WRONG_URL;
            string script = TEST_SCRIPT;

            //Act
            var result = await scrapper.GetResultOfScript<object>(url, script);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetResultOfScript_ExistingUrlButWrongScript_ReturnsNull()
        {
            //Arrange
            Scrapper scrapper = new();
            string url = TEST_URL;
            string script = TEST_WRONG_SCRIPT;

            //Act
            var result = await scrapper.GetResultOfScript<object>(url, script);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetResultOfScript_ExistingUrlAndGOODScript_Returnsnull()
        {
            //Arrange
            Scrapper scrapper = new();
            string url = TEST_URL;
            string script = TEST_SCRIPT;

            //Act
            var result = await scrapper.GetResultOfScript<object>(url, script);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetResultOfScript_ExistingUrlAndGOODScriptwithresult_ReturnsObject()
        {
            //Arrange
            Scrapper scrapper = new();
            string url = TEST_URL;
            string script = TEST_SCRIPT_WITH_RESULT;

            //Act
            var result = await scrapper.GetResultOfScript<object>(url, script);

            //Assert
            result.Should().NotBeNull();
        }

        #endregion
    }
}
