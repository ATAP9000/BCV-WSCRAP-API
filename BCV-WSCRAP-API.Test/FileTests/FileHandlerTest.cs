using BCV_WSCRAP_API.Utilities;
using FluentAssertions;
using System.Text;

namespace BCV_WSCRAP_API.Test.FileTests
{

    public class FileHandlerTest
    {
        #region [GetFile Method]

        [Fact]
        public void FileHandler_GetFile_ReturnString()
        {
            //Arrange
            string filename = "Fake";

            //Act
            var result = FileHandler.GetFile(filename);

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void FileHandler_GetFile_ReturnEmptyStringIfFileNotFound()
        {
            //Arrange
            string filename = "ThisFileDoesntExist.txt";

            //Act
            var result = FileHandler.GetFile(filename);

            //Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void FileHandler_GetFile_ReturnStringIfFileExist()
        {
            //Arrange
            string filePath = Path.Combine("Resources", "ThisFileExist.txt");

            //Act
            var result = FileHandler.GetFile(filePath);

            //Assert
            result.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void FileHandler_GetFile_ReturnStringIfFileNameIsNull()
        {
            //Arrange
            string filename = null;

            //Act
            var result = FileHandler.GetFile(filename);

            //Assert
            result.Should().NotBeNull();
        }

        #endregion

        #region [SaveFile Method]

        [Fact]
        public void FileHandler_SaveFile_ValidateFileExist()
        {
            //Arrange
            string filename = "TestFileToDelete";
            Stream stream = new MemoryStream(Encoding.UTF8.GetBytes("Test Text"));
            if (File.Exists(filename))
                File.Delete(filename);

            //Act
            FileHandler.SaveFile(stream,filename);

            //Assert
            File.Exists(filename).Should().BeTrue();
        }

        [Fact]
        public void FileHandler_SaveFile_ValidateFileDoesntExistIfStreamIsNull()
        {
            //Arrange
            string filename = "TestFileNotExisting";
            Stream stream = null;

            //Act
            FileHandler.SaveFile(stream,filename);

            //Assert
            File.Exists(filename).Should().BeFalse();    
        }

        [Fact]
        public void FileHandler_SaveFile_ValidateFileDoesntExistIfFilenameIsNull()
        {
            //Arrange
            string filename = null;
            Stream stream = new MemoryStream(Encoding.UTF8.GetBytes("Test Text"));

            //Act
            FileHandler.SaveFile(stream, filename);

            //Assert
            File.Exists(filename).Should().BeFalse();
        }

        #endregion
    }
}
