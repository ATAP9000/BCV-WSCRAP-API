using BCV_WSCRAP_API.Utilities;
using FluentAssertions;
using System.Text;

namespace BCV_WSCRAP_API.Test.UtilitiesTests
{

    public class FileHandlerTest
    {
        #region [GetFile Method]

        [Fact]
        public void GetFile_NonExistentFile_ReturnsEmptyString()
        {
            //Arrange
            string filename = "ThisFileDoesntExist.txt";

            //Act
            var result = FileHandler.GetFile(filename);

            //Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void GetFile_ExistentFile_ReturnsString()
        {
            //Arrange
            string filePath = Path.Combine("Resources", "ThisFileExist.txt");

            //Act
            var result = FileHandler.GetFile(filePath);

            //Assert
            result.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void GetFile_NullString_ReturnsEmptyString()
        {
            //Arrange
            string? filename = null;

            //Act
            var result = FileHandler.GetFile(filename);

            //Assert
            result.Should().NotBeNull();
        }

        #endregion

        #region [SaveFile Method]

        [Fact]
        public void SaveFile_ExistentFileWithName_ReturnsTrue()
        {
            //Arrange
            string filename = "TestFileToDelete";
            Stream stream = new MemoryStream(Encoding.UTF8.GetBytes("Test Text"));
            if (File.Exists(filename))
                File.Delete(filename);

            //Act
            FileHandler.SaveFile(stream, filename);

            //Assert
            File.Exists(filename).Should().BeTrue();
        }

        [Fact]
        public void SaveFile_NonExistentFileWithName_ReturnsFalse()
        {
            //Arrange
            string filename = "TestFileNotExisting";
            Stream? stream = null;

            //Act
            FileHandler.SaveFile(stream, filename);

            //Assert
            File.Exists(filename).Should().BeFalse();
        }

        [Fact]
        public void SaveFile_ExistentFileWithoutName_ReturnsFalse()
        {
            //Arrange
            string? filename = null;
            Stream stream = new MemoryStream(Encoding.UTF8.GetBytes("Test Text"));

            //Act
            FileHandler.SaveFile(stream, filename);

            //Assert
            File.Exists(filename).Should().BeFalse();
        }

        #endregion
    }
}
