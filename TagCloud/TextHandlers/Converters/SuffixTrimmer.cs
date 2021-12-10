using System;
using System.Collections.Generic;
using System.Linq;

namespace TagCloud.TextHandlers.Converters
{
    public class SuffixTrimmer : IWordConverter
    {
        public IEnumerable<string> Convert(IEnumerable<string> words)
        {
            return words.Select(Convert);
        }

        public string Convert(string word)
        {
            var apostropheLocation = word.IndexOf('\'');
            if (apostropheLocation != -1)
            {
                word = word.Substring(0, apostropheLocation);
            }

            return word;
        }
    }
}