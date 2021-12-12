using System.IO;
using System.Text;

namespace TagCloud.TextProcessing
{
    public class TxtFileReader : IFileReader
    {
        public string ReadFile(string path)
        {
            try
            {
                using var reader = new StreamReader(path, Encoding.UTF8);
                var text = reader.ReadToEnd();

                return text;
            }
            catch (IOException e)
            {
                throw new IOException($"Файл по пути {path} не был найден или не соответствует кодировкe UTF-8", e);
            }
        }
    }
}