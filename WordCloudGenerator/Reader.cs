using System.IO;

namespace WordCloudGenerator
{
    public class Reader
    {
        public string ReadFile(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException($"Файл {path} не найден");

            using var reader = new StreamReader(path);

            return reader.ReadToEnd();
        }
    }
}