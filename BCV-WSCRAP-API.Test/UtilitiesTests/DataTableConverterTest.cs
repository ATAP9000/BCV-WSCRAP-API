using BCV_WSCRAP_API.Test.Models;
using BCV_WSCRAP_API.Utilities;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BCV_WSCRAP_API.Test.UtilitiesTests
{
    public class DataTableConverterTest
    {
        private readonly DataTableConverter _dataTableConverter;

        private const int ZERO = 0;
        private const int EXPECTED_RESULT = 2;
        private const string APPSETTINGS_FILE = "Testappsettings.json";
        private const string RESOURCE_FOLDER = "Resources";
        private const string BADFORMATED_PAGE = "BadFormatedPage.html";
        private const string HTMLTABLE_PAGE = "HtmlTable.html";
        private const string NO_HEADER_HTMLTABLE_PAGE = "HtmlTableNoData.html";
        private const string NO_DATA_HTMLTABLE_PAGE = "HtmlTableNoHeader.html";

        public DataTableConverterTest()
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(APPSETTINGS_FILE).Build();
            _dataTableConverter = new DataTableConverter(configuration);
        }

        #region [HtmlToDataTable Method]

        [Fact]
        public void HtmlToDataTable_NullString_ReturnsEmptyDataTable()
        {
            //Arrange
            string? htmlString = null;

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
            string htmlString = GetTestFile(Path.Combine(RESOURCE_FOLDER, BADFORMATED_PAGE));

            //Act
            var result = _dataTableConverter.HtmlToDataTable(htmlString);

            //Assert
            result.Should().HaveRowCount(ZERO);
        }

        [Fact]
        public void HtmlToDataTable_HtmlTableWithoutHeader_ReturnsEmptyDataTable()
        {
            //Arrange
            string htmlString = GetTestFile(Path.Combine(RESOURCE_FOLDER, NO_HEADER_HTMLTABLE_PAGE));

            //Act
            var result = _dataTableConverter.HtmlToDataTable(htmlString);

            //Assert
            result.Should().HaveRowCount(ZERO);
        }

        [Fact]
        public void HtmlToDataTable_HtmlTableWithoutData_ReturnsEmptyDataTable()
        {
            //Arrange
            string htmlString = GetTestFile(Path.Combine(RESOURCE_FOLDER, NO_DATA_HTMLTABLE_PAGE));

            //Act
            var result = _dataTableConverter.HtmlToDataTable(htmlString);

            //Assert
            result.Should().HaveRowCount(ZERO);
        }

        [Fact]
        public void HtmlToDataTable_HtmlTable_ReturnsDataTable()
        {
            //Arrange
            string htmlString = GetTestFile(Path.Combine(RESOURCE_FOLDER, HTMLTABLE_PAGE));

            //Act
            var result = _dataTableConverter.HtmlToDataTable(htmlString, false);

            //Assert
            result.Should().HaveRowCount(EXPECTED_RESULT);
        }

        #endregion

        #region [DataTableToList Method]

        [Fact]
        public void DataTableToList_NullDataTable_ReturnsEmptyList()
        {
            //Arrange
            DataTable? dt = null;

            //Act
            var result = _dataTableConverter.DataTableToList<object>(dt);

            //Assert
            result.Should().HaveCount(ZERO);
        }

        [Fact]
        public void DataTableToList_EmptyDataTable_ReturnsList()
        {
            //Arrange
            DataTable dt = new();

            //Act
            var result = _dataTableConverter.DataTableToList<object>(dt);

            //Assert
            result.Should().HaveCount(ZERO);
        }

        [Fact]
        public void DataTableToList_DataTableWithData_ReturnsList()
        {
            //Arrange
            DataTable dt = new();
            dt.Columns.Add("NickName", typeof(string));
            dt.Columns.Add("Age", typeof(string));
            dt.Columns.Add("BirthDate", typeof(string));
            dt.Rows.Add("Jose", "25,4", "01-03-1998");
            dt.Rows.Add("Lucas", "50", "01/05/1922");

            //Act
            var result = _dataTableConverter.DataTableToList<Person>(dt);

            //Assert
            result.Should().HaveCount(EXPECTED_RESULT);
        }

        #endregion

        #region [ UTILS ]

        private string GetTestFile(string fileName)
        {
            var directory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var path = Path.Combine(directory!, fileName);
            return File.ReadAllText(path);
        }

        #endregion
    }
}
