using System;
using System.Collections.Generic;

namespace TagsCloudContainer.TextParsing.CloudParsing.ParsingRules
{
    public class DefaultParsingRule : ICloudWordParsingRule
    {
        private HashSet<string> wordsExceptions;

        public DefaultParsingRule()
        {
            wordsExceptions = new HashSet<string>
            {
                "i", "you", "he", "she", "who", "it", "we", "they",
                "me", "you", "whom", "her", "him", "it", "us", "them",
                "my", "mine", "his", "her", "hers", "your", "yours", 
                "our", "ours", "their", "theirs", "whose", "it", "its"
            };
        }
        public bool Check(string word)
        {
            if (string.IsNullOrWhiteSpace(word)) return false;
            var lower = word.ToLower();
            return !wordsExceptions.Contains(lower);
        }

        public string Apply(string word)
        {
            if (word == null) throw new ArgumentNullException(nameof(word));
            return word.ToLower();
        }
    }
}