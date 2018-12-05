using System.Collections.Generic;
using System.Linq;
using TagCloud.Util;
using TagCloud.WordsProcessors.ProcessingUtilities;

namespace TagCloud.WordsProcessors
{
    public abstract class AbstractWordsProcessor
    {
        protected readonly IProcessingUtility[] utilities;
        protected HashSet<string> boringWords;

        protected AbstractWordsProcessor(IProcessingUtility[] utilities, IEnumerable<string> boringWords)
        {
            this.utilities = utilities;
            this.boringWords = boringWords.ToHashSet();
        }

        public abstract List<TagStat> Process(List<string> words);

        protected static List<TagStat> HandleWordsCounter(Dictionary<string, int> wordsCounter)
        {
            var res = new List<TagStat>();
            foreach (var word in wordsCounter.Keys)
            {
                var count = wordsCounter[word];
                res.Add(new TagStat(word, count));
            }

            return res;
        }
    }
}