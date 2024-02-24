using HtmlAgilityPack;
using System.Data;
using System.Globalization;
using System.Reflection;

namespace BCV_WSCRAP_API.Utilities
{
    public class DataTableConverter
    {
        private readonly KeyPhrasesConverter _keyPhrasesConverter;

        private readonly CultureInfo _targetCulture;

        public DataTableConverter(IConfiguration configuration)
        {
            _keyPhrasesConverter = new(configuration);
            _targetCulture = new CultureInfo(configuration["TargetCulture"], false);
        }

        public DataTable HtmlToDataTable(string htmlCode)
        {
            HtmlDocument doc = new();
            doc.LoadHtml(htmlCode);
            var headers = doc.DocumentNode.SelectNodes("//tr/th");
            DataTable table = new();
            foreach (HtmlNode header in headers)
                table.Columns.Add(_keyPhrasesConverter.EvaluatePhrase(header.InnerText.Trim()));

            foreach (var row in doc.DocumentNode.SelectNodes("//tr[td]"))
                table.Rows.Add(row.SelectNodes("td").Select(td => td.InnerText.Trim()).ToArray());
            return table;
        }

        public List<T> DataTableToList<T>(DataTable dt)
        {
            List<T> data = [];
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
                            if(dr[column.ColumnName].ToString().Contains('-'))
                                pro.SetValue(obj, DateTime.ParseExact(dr[column.ColumnName].ToString().Replace("-","/"), _targetCulture.DateTimeFormat.ShortDatePattern, CultureInfo.InvariantCulture), null);
                            else
                                pro.SetValue(obj, DateTime.ParseExact(dr[column.ColumnName].ToString(), _targetCulture.DateTimeFormat.ShortDatePattern, CultureInfo.InvariantCulture), null);
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
    }
}
