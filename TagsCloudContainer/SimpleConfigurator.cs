using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer
{
    public class SimpleConfigurator : ICloudConfigurator
    {
        private readonly Dictionary<string, int> wordsWithFrequency;

        public SimpleConfigurator(IWordsPreprocessor preprocessor)
        {
            wordsWithFrequency = preprocessor.Prepare();
        }

        public IEnumerable<KeyValuePair<string, int>> ConfigureWords()
        {
            var fontSize = 16;
            var multiplier = 3;
            return wordsWithFrequency
                .OrderByDescending(kv => kv.Value)
                .Select(kv => new KeyValuePair<string, int>(kv.Key, kv.Value * multiplier + fontSize));
        }
    }
}
