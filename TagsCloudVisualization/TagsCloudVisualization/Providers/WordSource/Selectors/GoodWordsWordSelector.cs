using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.WordSource.Interfaces;

namespace TagsCloudVisualization.WordSource.Selectors
{
    internal class GoodWordsWordSelector : IWordSelector
    {
        private readonly HashSet<string> badSet;

        public GoodWordsWordSelector(IEnumerable<string> badWords)
        {
            badSet = badWords.ToHashSet();
        }

        public bool Select(string obj)
        {
            return !badSet.Contains(obj);
        }
    }
}