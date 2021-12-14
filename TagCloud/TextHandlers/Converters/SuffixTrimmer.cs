using System;

namespace TagCloud.TextHandlers.Converters
{
    public class SuffixTrimmer : IConverter
    {
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