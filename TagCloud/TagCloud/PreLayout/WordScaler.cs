using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloud.PreLayout
{
    internal static class WordScaler
    {
        public static List<Word> GetWordsWithScaledFontSize(Dictionary<string, int> wordsWithFrequency,
            float baseFontSize,
            FontFamily fontFamily)
        {
            var listWords = new List<Word>();
            var minFontSize = double.PositiveInfinity;

            foreach (var (word, frequency) in wordsWithFrequency)
            {
                var fontSize = Scale(frequency, baseFontSize);
                minFontSize = Math.Min(minFontSize, fontSize);
                listWords.Add(new Word(word, new Font(fontFamily, fontSize)));
            }

            return NormalizeFontSize(minFontSize, listWords, baseFontSize).ToList();

            float Scale(int freq, float fontSize)
            {
                return (float) (Math.Pow(freq - 1, 0.65) + 1) * fontSize;
            }

            IEnumerable<Word> NormalizeFontSize(double minSize, IEnumerable<Word> words, float baseFontSize)
            {
                return words.Select(w => w.WithFontSize(w.Font.Size / minSize * baseFontSize));
            }
        }
    }
}