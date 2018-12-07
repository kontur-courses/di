using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer
{
    public class SimpleWordCounter : IWordCounter
    {
        private IEnumerable<string> Words { get; }

        public SimpleWordCounter(IPreprocessor preprocessor)
        {
            Words = preprocessor.Process();
        }

        public IDictionary<string, int> GetWordsFrequency()
        {
            return Words
                .Where(x => x != string.Empty)
                .GroupBy(x => x)
                .ToDictionary(x => x.Key, x => x.Count());
        }
    }
}