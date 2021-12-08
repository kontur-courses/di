using System.IO;

namespace TagCloud.TextProcessing
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