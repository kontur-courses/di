using System.IO;

namespace TagCloud
{
    public class TextReader
    {
        public string Read(string path)
        {
            using var reader = new StreamReader(path);
            var text = reader.ReadToEnd();

            return text;
        }
    }
}