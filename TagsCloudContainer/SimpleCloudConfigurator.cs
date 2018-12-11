using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;

namespace TagsCloudContainer
{
    public class SimpleCloudConfigurator : ICloudConfigurator
    {
        private readonly Dictionary<string, int> wordsWithFrequency;

        public SimpleCloudConfigurator(IWordsPreprocessor preprocessor)
        {
            wordsWithFrequency = preprocessor.PrepareWords();
        }

        public IEnumerable<KeyValuePair<string, int>> ConfigureCloud()
        {
            var fontSize = 8;
            var maxFontSize = 60;
            var multiplier = 1;
            var maxWordsNumber = 200;

            return wordsWithFrequency
                .OrderByDescending(kv => kv.Value)
                .Take(maxWordsNumber)
                .Select(kv => new KeyValuePair<string, int>(kv.Key, Math.Min(kv.Value * multiplier + fontSize, maxFontSize)));
        }
    }
}
