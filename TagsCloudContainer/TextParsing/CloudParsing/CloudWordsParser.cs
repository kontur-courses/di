using System.Collections.Generic;

namespace TagsCloudContainer.TextParsing.CloudParsing
{
    public class CloudWordsParser
    {
        private Dictionary<string, CloudWord> words;
        private CloudWordsParserSettings settings;
       

        public CloudWordsParser(CloudWordsParserSettings settings)
        {
            words = new Dictionary<string, CloudWord>();
            this.settings = settings;
        }

        public IEnumerable<CloudWord> Parse()
        {
            foreach (var word in settings.FileWordsParser.ParseFrom(settings.Path))
            {
                if (!settings.Rule.Check(word)) continue;
                var appliedRule = settings.Rule.Apply(word);
                if (words.ContainsKey(appliedRule))
                    words[appliedRule].AddCount();
                else
                    words[appliedRule] = new CloudWord(appliedRule);
            }

            return words.Values;
        }
    }
}