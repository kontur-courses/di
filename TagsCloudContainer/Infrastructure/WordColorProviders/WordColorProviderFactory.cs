using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.Infrastructure
{
    public static class WordColorProviderFactory
    {
        public static IWordColorProvider CreateDefault(Dictionary<string, int> wordFrequences, Color minFrequencyColor, Color maxFrequencyColor)
        {
            return new WordColorProvider();
        }

        public static IWordColorProvider CreateDefault() => new WordColorProvider();
    }
}