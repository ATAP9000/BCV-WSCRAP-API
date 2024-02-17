using System.IO;
using System.Reflection;
using System.Text;

namespace BCV_WSCRAP_API.Utilities
{
    public static class FileHandler
    {
        public static string CurrentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public static string GetFile(string fileName)
        {
            StringBuilder htmlString = new();
            string path = CurrentPath + "\\" + fileName;
            string[] lines = File.ReadAllLines(path);
            foreach (string line in lines)
                htmlString.AppendLine(line.Trim());
            return htmlString.ToString();
        }

        public static void SaveFile(Stream stream, string fileName)
        {
            string path = CurrentPath + "\\" + fileName;
            using (var fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                stream.CopyTo(fs);
            }
        }
    }
}
