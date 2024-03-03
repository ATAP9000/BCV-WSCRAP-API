using BCV_WSCRAP_API.Utilities;
using FluentAssertions;
using Microsoft.Extensions.Configuration;

namespace BCV_WSCRAP_API.Test.UtilitiesTests
{
    public class KeyPhrasesConverterTest
    {
        private readonly KeyPhrasesConverter _keyPhraseConverter;

        public KeyPhrasesConverterTest()
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("Testappsettings.json").Build();
            _keyPhraseConverter = new(configuration);
        }

        #region [EvaluatePhrase Method]

        [Fact]
        public void EvaluatePhrase_EmptyPhrase_ReturnsEmptyString()
        {
            //Arrange
            string phrase = string.Empty;

            //Act
            var result = _keyPhraseConverter.EvaluatePhrase(phrase);

            //Assert
            result.Should().BeEmpty();

        }

        [Fact]
        public void EvaluatePhrase_NullPhrase_ReturnsEmptyString()
        {
            //Arrange
            string? phrase = null;

            //Act
            var result = _keyPhraseConverter.EvaluatePhrase(phrase);

            //Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void EvaluatePhrase_NonEmptyPhrase_ReturnsString()
        {
            //Arrange
            string phrase = "TestPhrase";

            //Act
            var result = _keyPhraseConverter.EvaluatePhrase(phrase);

            //Assert
            result.Should().NotBeNullOrEmpty();
        }
        #endregion
    }
}

