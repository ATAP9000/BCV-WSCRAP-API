using System.Data;

namespace BCV_WSCRAP_API.Services
{
    public interface IDataTableConverter
    {
        public IKeyPhrasesConverter _keyPhrasesConverter { get; set; }

        public DataTable HtmlToDataTable(string htmlCode);
        public List<T> DataTableToList<T>(DataTable dataTable);
    }
}
