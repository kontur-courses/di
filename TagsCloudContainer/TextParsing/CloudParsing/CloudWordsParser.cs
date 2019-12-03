using System;
using System.Collections.Generic;
using TagsCloudContainer.ApplicationRunning;

namespace TagsCloudContainer.TextParsing.CloudParsing
{
    public class CloudWordsParser : ICloudWordsParser
    {
        private Dictionary<string, CloudWord> words;
        private Func<CloudWordsParserSettings> settingsFactory;
       

        public CloudWordsParser(Func<CloudWordsParserSettings> settingsFactory)
        {
            words = new Dictionary<string, CloudWord>();
            this.settingsFactory = settingsFactory;
        }

        public IEnumerable<CloudWord> Parse()
        {
            var settings = settingsFactory();
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