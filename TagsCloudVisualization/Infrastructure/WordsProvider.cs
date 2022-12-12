using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TagsCloudVisualization.Infrastructure.Parsers;

namespace TagsCloudVisualization.Infrastructure
{
    public class WordsProvider : IWordsProvider
    {
        private static readonly Regex TextTypeRegex = new("\\.\\w+$", RegexOptions.Compiled);
        private readonly Dictionary<string, IParser> parsers;

        public WordsProvider(IEnumerable<IParser> parsers)
        {
            this.parsers = parsers.ToDictionary(p => p.FileType);
        }

        public IEnumerable<string> GetWords(string path)
        {
            var textType = TextTypeRegex.Match(path).Value;

            if (textType.Length == 0 || !parsers.ContainsKey(textType[1..]))
                throw new ArgumentException($"not found parser for {textType}");

            return parsers[textType[1..]].WordParse(path);
        }
    }
}