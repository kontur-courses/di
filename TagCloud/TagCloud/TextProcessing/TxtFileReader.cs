using System.IO;

namespace TagCloud.TextProcessing
{
    public class TxtFileReader : IFileReader
    {
        public string ReadFile(string path)
        {
            using var reader = new StreamReader(path);
            var text = reader.ReadToEnd();
            
            return text;
        }
    }
}