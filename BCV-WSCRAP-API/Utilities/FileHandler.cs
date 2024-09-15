using System.Reflection;
using System.Text;

namespace BCV_WSCRAP_API.Utilities
{
    /// <summary>Handles the usage of js files by the Scrapper</summary>
    public static class FileHandler
    {
        private static readonly string CurrentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;

        public static string GetFile(string? filePath)
        {
            StringBuilder result = new();
            try
            {
                string path = Path.Combine(CurrentPath, filePath!);
                foreach (var line in File.ReadLines(path))
                    result.AppendLine(line.Trim());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result.ToString();
        }
    }
}
