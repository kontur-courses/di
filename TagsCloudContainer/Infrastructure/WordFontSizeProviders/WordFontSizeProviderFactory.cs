using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.Infrastructure
{
    public static class WordFontSizeProviderFactory
    {
        public static IWordFontSizeProvider CreateDefault(Dictionary<string, int> wordFrequences, float minFrequencyFontSize, float maxFrequencyFontSize)
        {
            return new WordFontSizeProvider();
        }
    }
}