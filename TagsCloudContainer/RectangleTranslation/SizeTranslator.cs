using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace TagsCloudContainer.RectangleTranslation
{
    public class SizeTranslator : ISizeTranslator
    {
        private const int MaxFontSize = 32;

        public IEnumerable<SizedWord> TranslateWordsToSizedWords(Dictionary<string, int> countedWords)
        {
            var result = new List<SizedWord>();
            var maxWordCount = countedWords.Max(countedWord => countedWord.Value);
            foreach (var countedWord in countedWords)
            {
                var word = countedWord.Key;
                var wordCount = countedWord.Value;
                var fontSize = (int) Math.Round((double) wordCount / maxWordCount * MaxFontSize);
                result.Add(new SizedWord(word, fontSize));
            }

            return result;
        }
    }
}