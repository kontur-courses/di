using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TagsCloud
{
    public static class WordsRectanglesScaler
    {
        public static Dictionary<string, double> ConvertFreqToPropotions(Dictionary<string, int> wordsFreq)
        {
            var scaledWords = new Dictionary<string, double>();
            var baseHeight = wordsFreq.First().Value;

            foreach (var word in wordsFreq)
            {
                scaledWords.Add(word.Key, (double)word.Value / baseHeight);
            }

            return scaledWords;
        }
    }
}
