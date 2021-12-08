using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TagsCloudApp
{
    public class DictionaryObjectParser<TKey, TValue> : IObjectParser<Dictionary<TKey, TValue>> where TKey : notnull
    {
        private static readonly Regex keyValueRegex = new(@"\(((?<key>[^ )]*) (?<value>[^)]*))*?\)");

        private readonly IObjectParser<TKey> keyParser;
        private readonly IObjectParser<TValue> valueParser;

        public DictionaryObjectParser(IObjectParser<TKey> keyParser, IObjectParser<TValue> valueParser)
        {
            this.keyParser = keyParser;
            this.valueParser = valueParser;
        }

        public Dictionary<TKey, TValue> Parse(string input)
        {
            var dictionary = new Dictionary<TKey, TValue>();
            foreach (Match match in keyValueRegex.Matches(input))
            {
                var keyMatch = match.Groups["key"].Value;
                var valueMatch = match.Groups["value"].Value;
                if (keyMatch == "" || valueMatch == "")
                    continue;

                var key = keyParser.Parse(keyMatch);
                var value = valueParser.Parse(valueMatch);
                dictionary[key] = value;
            }

            return dictionary;
        }
    }
}