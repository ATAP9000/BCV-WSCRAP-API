using System.Reflection;
using System.Text;

namespace BCV_WSCRAP_API.Utilities
{
    public static class FileHandler
    {
        private static readonly string CurrentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public static string GetFile(string fileName)
        {
            string path = CurrentPath + "\\" + fileName;
            StringBuilder result = new();
            foreach (var line in File.ReadLines(path))
                result.AppendLine(line.Trim());
            return result.ToString();
        }

        public static void SaveFile(Stream stream, string fileName)
        {
            string path = CurrentPath + "\\" + fileName;
            using var fileStream = new FileStream(path, FileMode.OpenOrCreate);
            stream.CopyTo(fileStream);
        }
    }
}
