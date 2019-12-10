using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace TagsCloudContainer.FileReading
{
    public class TextFileReader : IFileReader
    {
        public IEnumerable<string> ReadWords(string textFileName)
        {
            var notAllowedSymbolsRegex = new Regex(@"[^\w+ ]");

            foreach (var line in File.ReadLines(textFileName))
            {
                var allowedLine = notAllowedSymbolsRegex.Replace(line, " ");
                foreach (var word in allowedLine.Split(' ').Where(w => w.Length > 0))
                {
                    yield return word;
                }
            }
        }
    }
}