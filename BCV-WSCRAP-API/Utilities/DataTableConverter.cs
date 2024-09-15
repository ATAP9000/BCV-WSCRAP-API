using HtmlAgilityPack;
using System.Data;
using System.Globalization;
using System.Reflection;

namespace BCV_WSCRAP_API.Utilities
{
    /// <summary>Converts values of html table into List or Datatables</summary>
    public class DataTableConverter
    {
        private readonly KeyPhrasesConverter _keyPhrasesConverter;

        private readonly CultureInfo _targetCulture;

        private const string TABLE_HEADER = "//tr/th";

        private const string TABLE_DATA = "//tr[td]";

        private const string TD = "td";

        private const string TARGET_CULTURE = "TargetCulture";

        private const string DEFAULT_CULTURE = "es-VE";


        public DataTableConverter(IConfiguration configuration)
        {
            _keyPhrasesConverter = new(configuration);
            _targetCulture = new CultureInfo(configuration[TARGET_CULTURE] ?? DEFAULT_CULTURE, false);
        }

        /// <summary>Converts Html into Datatable</summary>
        /// <param name="htmlCode">HTML string</param>
        /// <param name="convertHeader">Indicates if the header of the html table will be used</param>
        /// <returns>HTML table converted into Datatable</returns>
        public DataTable HtmlToDataTable(string? htmlCode, bool convertHeader = true)
        {
            DataTable dt = new();
            if (string.IsNullOrEmpty(htmlCode))
                return dt;

            HtmlDocument doc = new();
            doc.LoadHtml(htmlCode);
            if (doc.ParseErrors.Any())
                return dt;

            ParseHtmlTableToDataTable(ref dt, doc, convertHeader);
            return dt;
        }

        /// <summary>Converts Datatable into List</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns>List of T</returns>
        public List<T> DataTableToList<T>(DataTable? dt)
        {
            List<T> data = [];

            if (dt == null || dt.Columns.Count <= 0 || dt.Rows.Count <= 0)
                return data;

            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }

            return data;
        }

        private T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                    {
                        if (pro.PropertyType == typeof(decimal))
                        {
                            decimal.TryParse(dr[column.ColumnName].ToString(), _targetCulture, out decimal value);
                            pro.SetValue(obj, value, null);
                        }
                        else if (pro.PropertyType == typeof(DateTime))
                        {
                            if(dr[column.ColumnName] == null)
                                pro.SetValue(obj, DateTime.MinValue);
                            else if (dr[column.ColumnName].ToString()!.Contains('-'))
                                pro.SetValue(obj, DateTime.ParseExact(dr[column.ColumnName].ToString()!.Replace("-","/"), _targetCulture.DateTimeFormat.ShortDatePattern, CultureInfo.InvariantCulture), null);
                            else
                                pro.SetValue(obj, DateTime.ParseExact(dr[column.ColumnName].ToString()!, _targetCulture.DateTimeFormat.ShortDatePattern, CultureInfo.InvariantCulture), null);
                        }
                        else
                            pro.SetValue(obj, Convert.ChangeType(dr[column.ColumnName], pro.PropertyType), null);
                    }
                    else
                        continue;
                }
            }
            return obj;
        }

        /// <summary>Parses Html Table into Datatable</summary>
        /// <param name="dt"></param>
        /// <param name="htmlDocument">HTML document</param>
        /// <param name="convertHeader">Indicates if the header of the html table will be used</param>
        private void ParseHtmlTableToDataTable(ref DataTable dt, HtmlDocument htmlDocument, bool convertHeader = true)
        {
            var headers = htmlDocument.DocumentNode.SelectNodes(TABLE_HEADER);
            var data = htmlDocument.DocumentNode.SelectNodes(TABLE_DATA);
            if (headers == null || data == null)
                return;

            foreach (HtmlNode header in headers)
                dt.Columns.Add(_keyPhrasesConverter.EvaluatePhrase(header.InnerText.Trim(), convertHeader));

            foreach (var row in data)
                dt.Rows.Add(row.SelectNodes(TD).Select(td => td.InnerText.Trim()).ToArray());
        }
    }
}
