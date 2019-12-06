using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudVisualization
{
    internal class WordsToSizableProvider : ISizableProvider<string>
    {
        public WordsToSizableProvider(ISizableSelector<string, int> selector)
        {
            Selector = selector;
        }

        public ISizableSelector<string, int> Selector { set; private get; }

        public IEnumerable<Sizable<string>> GetSizableObjects(
            Dictionary<string, int> wordFrequency, int maxCountWords)
        {
            return wordFrequency.OrderByDescending(pair => pair.Value).Take(maxCountWords).Select(kv =>
                    new Sizable<string>(kv.Key, Selector.GetSize(kv.Key, kv.Value)))
                .Where(sizable => sizable.DrawSize.Width > 0 && sizable.DrawSize.Height > 0);
        }
    }
}