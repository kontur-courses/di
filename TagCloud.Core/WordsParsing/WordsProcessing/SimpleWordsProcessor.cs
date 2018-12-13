using System;
using System.Collections.Generic;
using System.Linq;
using TagCloud.Core.Util;
using TagCloud.Core.WordsParsing.WordsProcessing.WordsProcessingUtilities;

namespace TagCloud.Core.WordsParsing.WordsProcessing
{
    public class SimpleWordsProcessor : IWordsProcessor
    {
        private readonly IWordsProcessingUtility[] utilities;

        public SimpleWordsProcessor(IWordsProcessingUtility[] utilities)
        {
            this.utilities = utilities;
        }
   
        public IEnumerable<TagStat> Process(IEnumerable<string> words, HashSet<string> boringWords = null, int? maxUniqueWordsCount = null)
        {
            if (words is null)
                throw new ArgumentNullException(nameof(words));

            var wordsCounter = new Dictionary<string, int>();
            var resWords = utilities.Aggregate(words, (current, processingUtility) => processingUtility.Process(current));
            foreach (var resWord in resWords)
            {
                if (boringWords != null && boringWords.Contains(resWord))
                    continue;
                var resCount = 1;
                if (wordsCounter.TryGetValue(resWord, out var currentCount))
                    resCount = currentCount + 1;
                wordsCounter[resWord] = resCount;
            }

            return HandleWordsCounter(wordsCounter, maxUniqueWordsCount);
        }

        private static IEnumerable<TagStat> HandleWordsCounter(Dictionary<string, int> wordsCounter, int? maxUniqueWordsCount)
        {
            var allTagsStats = new List<TagStat>();
            foreach (var word in wordsCounter.Keys)
            {
                var count = wordsCounter[word];
                allTagsStats.Add(new TagStat(word, count));
            }
            
            return maxUniqueWordsCount.HasValue
                ? allTagsStats.OrderBy(ts => ts.RepeatsCount).Take(maxUniqueWordsCount.Value).ToList()
                : allTagsStats;
        }
    }
}