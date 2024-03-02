using BCV_WSCRAP_API.Utilities;
using FluentAssertions;
using Microsoft.Extensions.Configuration;

namespace BCV_WSCRAP_API.Test.DataTableTests
{
    public class DataTableConverterTest
    {
        private readonly DataTableConverter _dataTableConverter;

        private readonly int ZERO = 0;

        public DataTableConverterTest()
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("Testappsettings.json").Build();
            _dataTableConverter = new DataTableConverter(configuration);
        }

        #region [HtmlToDataTable Method]

        [Fact]
        public void HtmlToDataTable_NullString_ReturnsEmptyDataTable()
        {
            //Arrange
            string htmlString = null;

            //Act
            var result = _dataTableConverter.HtmlToDataTable(htmlString);

            //Assert
            result.Should().HaveRowCount(ZERO);
        }

        [Fact]
        public void HtmlToDataTable_EmptyString_ReturnsEmptyDataTable()
        {
            //Arrange
            string htmlString = string.Empty;

            //Act
            var result = _dataTableConverter.HtmlToDataTable(htmlString);

            //Assert
            result.Should().HaveRowCount(ZERO);
        }

        [Fact]
        public void HtmlToDataTable_BadFormatedHtml_ReturnsEmptyDataTable()
        {
            //Arrange
            string htmlString = "<!DOCTYPE html>\r\n<html>\r\n<head>\r\n    <title>Badly Formatted HTML</title>\r\n</head>\r\n<body>\r\n    <h1>Welcome to My Website</h1>\r\n    <p>This is a paragraph with <strong>unclosed tags and missing attributes.</p>\r\n    <ul>\r\n        <li>Item 1\r\n        <li>Item 2\r\n        <li>Item 3\r\n    </ul>\r\n    <img src=\"image.jpg\" alt=\"Image\" width=300 height=200>\r\n    <table>\r\n        <tr>\r\n            <td>Cell 1\r\n            <td>Cell 2\r\n        <tr>\r\n            <td>Cell 3\r\n            <td>Cell 4\r\n    </table>\r\n    <div>This <span>div</div> is not properly closed.\r\n</body>\r\n</html>";

            //Act
            var result = _dataTableConverter.HtmlToDataTable(htmlString);

            //Assert
            result.Should().HaveRowCount(ZERO);
        }

        //[Fact]
        //public void HtmlToDataTable_HtmlTableWithoutHeader_ReturnsEmptyDataTable()
        //{
        //    //Arrange
        //    string htmlString = string.Empty;

        //    //Act
        //    var result = _dataTableConverter.HtmlToDataTable(htmlString);

        //    //Assert
        //    result.Should().HaveRowCount(ZERO);
        //}

        //[Fact]
        //public void HtmlToDataTable_HtmlTableWithoutData_ReturnsEmptyDataTable()
        //{
        //    //Arrange
        //    string htmlString = string.Empty;

        //    //Act
        //    var result = _dataTableConverter.HtmlToDataTable(htmlString);

        //    //Assert
        //    result.Should().HaveRowCount(ZERO);
        //}

        //[Fact]
        //public void HtmlToDataTable_HtmlString_ReturnsDataTable()
        //{
        //    //Arrange
        //    string filename = "Fake";

        //    //Act
        //    var result = _dataTableConverter.DataTableToList<object>(new System.Data.DataTable());

        //    //Assert
        //    result.Should().NotBeNull();
        //}

        //#endregion

        //#region [DataTableToList Method]

        //[Fact]
        //public void DataTableToList_NullDataTable_ReturnsEmptyList()
        //{
        //    //Arrange
        //    string filename = "Fake";

        //    //Act
        //    var result = _dataTableConverter.DataTableToList<object>(new System.Data.DataTable());

        //    //Assert
        //    result.Should().NotBeNull();
        //}

        //[Fact]
        //public void DataTableToList_DataTable_ReturnsList()
        //{
        //    //Arrange
        //    string filename = "Fake";

        //    //Act
        //    var result = _dataTableConverter.DataTableToList<object>(new System.Data.DataTable());

        //    //Assert
        //    result.Should().NotBeNull();
        //}

        #endregion

    }
}
