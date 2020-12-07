using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TagsCloudContainer.Infrastructure.TextParserToFrequencyDictionary;

namespace TagsCloudContainer.App.TextParserToFrequencyDictionary
{
    class LiteratureTextParser : ITextParser
    {
        private readonly Regex wordRegex = new Regex(@"\p{IsCyrillic}+");
        public IEnumerable<string> GetWords(IEnumerable<string> lines)
        {
            return lines.SelectMany(line => wordRegex.Matches(line)).Select(match => match.Value);
        }
    }
}
