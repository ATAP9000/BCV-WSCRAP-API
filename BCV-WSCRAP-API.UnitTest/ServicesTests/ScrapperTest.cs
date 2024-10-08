﻿using BCV_WSCRAP_API.Services;
using FluentAssertions;
using Microsoft.Extensions.Configuration;

namespace BCV_WSCRAP_API.Test.ServicesTests
{
    [Collection("Puppeteer collection")]
    public class ScrapperTest 
    {
        private const string TEST_SCRIPT = "() => { console.log('hi'); }";
        private const string TEST_SCRIPT_WITH_RESULT = "() => { return 1; }";
        private const string TEST_URL = "https://www.google.com/";
        private const string TEST_WRONG_URL = "https://www.google.come/";
        private const string TEST_WRONG_SCRIPT = "console.log('hi')";
        public IConnectionStrings connectionStrings;
        public IConfiguration configuration;

        public ScrapperTest()
        {
            configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("Testappsettings.json").Build();
            connectionStrings = new ConnectionStrings(configuration.GetSection("ConnectionStrings"));
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

        #region [GetResultOfScriptWithReload Method]
        [Fact]
        public async Task GetResultOfScriptWithReload_NoUrlNoScript_ReturnsNull()
        {
            //Arrange
            Scrapper scrapper = new();
            string url = string.Empty;
            string script = string.Empty;

            //Act
            var result = await scrapper.GetResultOfScriptWithReload<object>(url, script);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetResultOfScriptWithReload_NoUrlButScript_ReturnsNull()
        {
            //Arrange
            Scrapper scrapper = new();
            string url = string.Empty;
            string script = TEST_SCRIPT;

            //Act
            var result = await scrapper.GetResultOfScriptWithReload<object>(url, script);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetResultOfScriptWithReload_UrlNoScript_ReturnsNull()
        {
            //Arrange
            Scrapper scrapper = new();
            string url = TEST_URL;
            string script = string.Empty;

            //Act
            var result = await scrapper.GetResultOfScriptWithReload<object>(url, script);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetResultOfScriptWithReload_NotExistingUrlButScript_ReturnsNull()
        {
            //Arrange
            Scrapper scrapper = new();
            string url = TEST_WRONG_URL;
            string script = TEST_SCRIPT;

            //Act
            var result = await scrapper.GetResultOfScriptWithReload<object>(url, script);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetResultOfScriptWithReload_ExistingUrlButWrongScript_ReturnsNull()
        {
            //Arrange
            Scrapper scrapper = new();
            string url = TEST_URL;
            string script = TEST_WRONG_SCRIPT;

            //Act
            var result = await scrapper.GetResultOfScriptWithReload<object>(url, script);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetResultOfScriptWithReload_ExistingUrlAndGOODScript_Returnsnull()
        {
            //Arrange
            Scrapper scrapper = new();
            string url = TEST_URL;
            string script = TEST_SCRIPT;

            //Act
            var result = await scrapper.GetResultOfScriptWithReload<object>(url, script);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetResultOfScriptWithReload_ExistingUrlAndGOODScriptwithresult_ReturnsObject()
        {
            //Arrange
            Scrapper scrapper = new();
            string url = TEST_URL;
            string script = TEST_SCRIPT_WITH_RESULT;

            //Act
            var result = await scrapper.GetResultOfScriptWithReload<object>(url, script);

            //Assert
            result.Should().NotBeNull();
        }
        #endregion
    }
}
