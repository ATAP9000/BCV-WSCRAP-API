using BCV_WSCRAP_API.Utilities;
using FluentAssertions;

namespace BCV_WSCRAP_API.Test.UtilitiesTests
{

    public class FileHandlerTest
    {
        #region [GetFile Method]

        [Fact]
        public void GetFile_NonExistentFilePath_ReturnsEmptyString()
        {
            //Arrange
            string filename = "ThisFileDoesntExist.txt";

            //Act
            var result = FileHandler.GetFile(filename);

            //Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void GetFile_FilePath_ReturnsString()
        {
            //Arrange
            string filePath = Path.Combine("Resources", "ThisFileExist.txt");

            //Act
            var result = FileHandler.GetFile(filePath);

            //Assert
            result.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void GetFile_Null_ReturnsEmptyString()
        {
            //Arrange
            string? filename = null;

            //Act
            var result = FileHandler.GetFile(filename);

            //Assert
            result.Should().BeEmpty();
        }

        #endregion
    }
}
