using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using MyStemWrapper;
using TagCloud.Infrastructure.Text.Information;

namespace TagCloud.Infrastructure.Text.Filters
{
    public class WordTypeFilter : IFilter<string>
    {
        private readonly string myStemPath;
        private readonly Regex wordWithTypeRegex;

        public WordTypeFilter(string myStemPath)
        {
            this.myStemPath = myStemPath;
            wordWithTypeRegex = new Regex(@".+?\{(?<word>.+?)=(?<type>.+?)\W.+?\}");
        }

        public IEnumerable<(string token, TokenInfo info)> Filter(IEnumerable<(string token, TokenInfo info)> tokens)
        {
            var analyzer = new MyStem {PathToMyStem = myStemPath, Parameters = "-i"};
            var analysis = analyzer.Analysis(string.Join(" ", tokens.Select(pair => pair.token)));
            foreach (Match match in wordWithTypeRegex.Matches(analysis))
            {
                var word = match.Groups["word"].Value;
                if (match.Success)
                {
                    if (!Enum.TryParse(match.Groups["type"].Value, out WordType wordType))
                        wordType = WordType.UNKNOWN;
                    yield return (word, new TokenInfo(wordType));
                }
            }
        }
    }
}