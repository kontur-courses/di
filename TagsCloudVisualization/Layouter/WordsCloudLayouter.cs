using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public class WordsCloudLayouter 
    {
        private readonly ICloudLayouter layouter;
        private readonly FontSettings fontSettings;
        private readonly float maxFontSize;
        private float maxWeight;
        private float minWeight;
        
        public int Radius => layouter.Radius;

        public WordsCloudLayouter(ICloudLayouter layouter, FontSettings fontSettings, float maxFontSize = 100f)
        {
            this.layouter = layouter;
            this.fontSettings = fontSettings;
            this.maxFontSize = maxFontSize;
        }

        public IEnumerable<Word> LayWords(IEnumerable<(string, int)> frequencyWords)
        {
            var wordsCount = frequencyWords.Select(s => s.Item2).ToList();
            maxWeight = wordsCount.Max();
            minWeight = wordsCount.Min();

            foreach (var word in frequencyWords)
            {
                var font = new Font(fontSettings.FontFamily, GetFontSize(word.Item2), fontSettings.FontStyle);
                yield return PutNextWord(word.Item1, font);
            }
        }

        private Word PutNextWord(string text, Font font)
        {
            if (text.Length == 0)
                throw new ArgumentException("text length should be grater than zero");
            var size = text.GetSurroundingRectangleSize(font);
            var word = new Word(text, font, layouter.PutNextRectangle(size));
            return word;
        }

        private float GetFontSize(float currentSize)
        {
            return currentSize > minWeight 
                ? (maxFontSize * (currentSize - minWeight)) / (maxWeight - minWeight) : 1;
        }
    }
}
