using System.Collections.Generic;

namespace TagsCloudContainer.TextParsing.CloudParsing
{
    public class CloudWordsParser
    {
        private Dictionary<string, CloudWord> words;

        public CloudWordsParser()
        {
            words = new Dictionary<string, CloudWord>();
        }

        public IEnumerable<CloudWord> ParseFrom(IFileWordsParser fileWordsParser, string path, ICloudWordParsingRule rule)
        {
            foreach (var word in fileWordsParser.ParseFrom(path))
            {
                if (!rule.Check(word)) continue;
                var appliedRule = rule.Apply(word);
                if (words.ContainsKey(appliedRule))
                    words[appliedRule].AddCount();
                else
                    words[appliedRule] = new CloudWord(appliedRule);
            }

            return words.Values;
        }
    }
}