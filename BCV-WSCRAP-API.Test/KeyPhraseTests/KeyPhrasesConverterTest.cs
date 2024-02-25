using BCV_WSCRAP_API.Utilities;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;

namespace BCV_WSCRAP_API.Test.KeyPhraseTests
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
        public void KeyPhrasesConverter_EvaluatePhrase_ReturnStringIfPhraseIsEmpty()
        {
            //Arrange
            string phrase = string.Empty;

            //Act
            var result = _keyPhraseConverter.EvaluatePhrase(phrase);

            //Assert
            result.Should().BeEmpty();

        }

        [Fact]
        public void KeyPhrasesConverter_EvaluatePhrase_ReturnStringIfPhraseIsNull()
        {
            //Arrange
            string phrase = null;

            //Act
            var result = _keyPhraseConverter.EvaluatePhrase(phrase);

            //Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void KeyPhrasesConverter_EvaluatePhrase_ReturnStringIfPhraseExist()
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

