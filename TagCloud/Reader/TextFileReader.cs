using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Xceed.Words.NET;

namespace TagCloud.Reader
{
    public class TextFileReader : IWordsFileReader
    {
        public static readonly Dictionary<string, Func<string, string>> FormatReaders = new Dictionary<string, Func<string, string>>
        {
            ["txt"] = ReadTxt,
            ["docx"] = ReadDocx
        };

        public IEnumerable<string> Read(string fileName)
        {
            var regex = new Regex("\\p{L}+");
            var format = Regex.Match(fileName, ".+\\.(.+)$").Groups[1].Value;
            var matches = regex.Matches(FormatReaders[format](fileName));
            foreach (Match match in matches)
                yield return match.Value;
        }

        private static string ReadTxt(string fileName) => File.ReadAllText(fileName, Encoding.Default);

        private static string ReadDocx(string fileName) => DocX.Load(fileName).Text;
    }
}