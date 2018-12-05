using System;
using System.Collections.Generic;
using TagCloud.Util;
using TagCloud.WordsProcessors.ProcessingUtilities;

namespace TagCloud.WordsProcessors
{
    public class SimpleWordsProcessor : AbstractWordsProcessor
    {
        public SimpleWordsProcessor(IProcessingUtility[] utilities, IEnumerable<string> boringWords)
            : base(utilities, boringWords)
        {
        }
   
        public override List<TagStat> Process(List<string> words)
        {
            if (words is null)
                throw new ArgumentException("List of unhandled words should not be null");

            var wordsCounter = new Dictionary<string, int>();
            foreach (var word in words)
            {
                var resWord = ApplyProcessingUtilitiesTo(word);
                if (resWord == null || boringWords.Contains(resWord))
                    continue;
                var resCount = 1;
                if (wordsCounter.TryGetValue(resWord, out var currentCount))
                    resCount = currentCount + 1;
                wordsCounter[resWord] = resCount;
            }

            return HandleWordsCounter(wordsCounter);
        }

        private string ApplyProcessingUtilitiesTo(string word)
        {
            var curWord = word;
            foreach (var utility in utilities)
            {
                curWord = utility.Handle(curWord);
                if (curWord == null)
                    break;
            }

            return curWord;
        }
    }
}