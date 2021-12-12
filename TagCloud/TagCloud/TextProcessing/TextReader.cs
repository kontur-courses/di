using System.IO;

namespace TagCloud.TextProcessing
{
    public interface ITextReader
    {
        string ReadFile(string path);
    }

    public class TextReader : ITextReader
    {
        public string ReadFile(string path)
        {
            using var reader = new StreamReader(path);
            var text = reader.ReadToEnd();
            
            return text;
        }
    }
    
}