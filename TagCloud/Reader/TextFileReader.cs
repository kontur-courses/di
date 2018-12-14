using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TagCloud.Reader.FormatReader;

namespace TagCloud.Reader
{
    public class TextFileReader : IWordsFileReader
    {
        private readonly Dictionary<string, IFormatReader> formatReaders;

        public TextFileReader(IEnumerable<IFormatReader> readers)
        {
            formatReaders = readers.ToDictionary(reader => reader.Format);
        }

        public static string GetFormat(string fileName) => Regex.Match(fileName, ".+\\.(.+)$").Groups[1].Value;

        public IEnumerable<string> ReadWords(string fileName)
        {
            var format = GetFormat(fileName);
            var text = formatReaders.TryGetValue(format, out var reader) 
                ? reader.Read(fileName) 
                : File.ReadAllText(fileName, Encoding.Default);
            var matches = new Regex("\\p{L}+").Matches(text);
            foreach (Match match in matches)
                yield return match.Value;
        }
    }
}