using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TagCloud.Reader
{
    public class TextFileReader : IWordsFileReader
    {
        public IEnumerable<string> Read(string fileName)
        {
            var strings = File.ReadAllText(fileName, Encoding.Default)
                .Split(' ', '.', ',', '?', '!', ':', ';', '-', '"', '\'', '\n', '\r', '\t', '(', ')', '<', '>');
            return strings
                .Where(word => !string.IsNullOrWhiteSpace(word))
                .Where(word => word.All(char.IsLetter));
        }
    }
}