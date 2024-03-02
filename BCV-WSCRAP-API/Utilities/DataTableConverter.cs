﻿using HtmlAgilityPack;
using System.Data;
using System.Globalization;
using System.Reflection;

namespace BCV_WSCRAP_API.Utilities
{
    public class DataTableConverter
    {
        private readonly KeyPhrasesConverter _keyPhrasesConverter;

        private readonly CultureInfo _targetCulture;

        private const string TABLE_HEADER = "//tr/th";

        private const string TABLE_DATA = "//tr[td]";

        private const string TD = "td";

        private const string TARGET_CULTURE = "TargetCulture";


        public DataTableConverter(IConfiguration configuration)
        {
            _keyPhrasesConverter = new(configuration);
            _targetCulture = new CultureInfo(configuration[TARGET_CULTURE], false);
        }

        public DataTable HtmlToDataTable(string htmlCode)
        {
            DataTable dt = new();
            if (string.IsNullOrEmpty(htmlCode))
                return dt;

            HtmlDocument doc = new();
            doc.LoadHtml(htmlCode);
            if (doc.ParseErrors.Any())
                return dt;

            ParseHtmlTableToDataTable(ref dt, doc);
            return dt;
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

        private void ParseHtmlTableToDataTable(ref DataTable dt, HtmlDocument htmlDocument)
        {
            var headers = htmlDocument.DocumentNode.SelectNodes(TABLE_HEADER);
            foreach (HtmlNode header in headers)
                dt.Columns.Add(_keyPhrasesConverter.EvaluatePhrase(header.InnerText.Trim()));

            foreach (var row in htmlDocument.DocumentNode.SelectNodes(TABLE_DATA))
                dt.Rows.Add(row.SelectNodes(TD).Select(td => td.InnerText.Trim()).ToArray());
        }
    }
}
