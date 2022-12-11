using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using TagsCloud.Interfaces;

namespace TagsCloud
{
    public class WordsRectanglesScaler : IWordsRectanglesScaler
    {
        private readonly IComparer<double> comparer;

        public WordsRectanglesScaler(IComparer<double> dictionaryComparer)
        {
            comparer = dictionaryComparer;
        }

        public SortedDictionary<double, List<string>> ConvertFreqToProportions(SortedDictionary<int, List<string>> wordsFreq)
        {
            var scaledWords = new SortedDictionary<double, List<string>>(comparer);
            var baseHeight = (double)wordsFreq.First().Key;

            foreach (var word in wordsFreq)
            {
                scaledWords.Add(word.Key / baseHeight, word.Value);
            }

            return scaledWords;
        }
    }
}
