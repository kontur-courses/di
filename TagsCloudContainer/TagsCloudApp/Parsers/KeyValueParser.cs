using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TagsCloudApp.Parsers
{
    public class KeyValueParser : IKeyValueParser
    {
        private static readonly Regex keyValueRegex = new(@"\(((?<key>[^ )]*) (?<value>[^)]*))*?\)");

        public IEnumerable<KeyValuePair<string, string>> Parse(string input)
        {
            foreach (Match match in keyValueRegex.Matches(input))
            {
                var keyMatch = match.Groups["key"].Value;
                var valueMatch = match.Groups["value"].Value;
                if (keyMatch == "" || valueMatch == "")
                    continue;

                yield return new KeyValuePair<string, string>(keyMatch, valueMatch);
            }
        }
    }
}