using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace TagCloud.Reader
{
    public class TextFileReader : IWordsFileReader
    {
        public IEnumerable<string> Read(string fileName)
        {
            var regex = new Regex("\\p{L}+");
            var matches = regex.Matches(File.ReadAllText(fileName, Encoding.Default));
            foreach (Match match in matches)
                yield return match.Value;
        }
    }
}