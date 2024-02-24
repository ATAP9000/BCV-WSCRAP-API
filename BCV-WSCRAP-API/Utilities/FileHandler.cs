using System.Reflection;
using System.Text;

namespace BCV_WSCRAP_API.Utilities
{
    public static class FileHandler
    {
        private static readonly string CurrentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public static string GetFile(string fileName)
        {
            StringBuilder result = new();
            try
            {
                string path = Path.Combine(CurrentPath, fileName);
                foreach (var line in File.ReadLines(path))
                    result.AppendLine(line.Trim());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result.ToString();
        }

        public static void SaveFile(Stream stream, string fileName)
        {
            try
            {
                if (stream == null)
                     throw new Exception("Stream Not Found");

                string path = Path.Combine(CurrentPath, fileName);
                using var fileStream = new FileStream(path, FileMode.OpenOrCreate);
                stream.CopyTo(fileStream);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
