using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TagsCloudVisualization.Infrastructure;

namespace TagsCloudVisualization.WordsProcessing
{
    public class WordSizeConverter : ISizeConverter
    {
        private readonly FontSettings fontSettings;
        
        public WordSizeConverter(FontSettings fontSettings)
        {
            this.fontSettings = fontSettings;
        }
        
        
        public IEnumerable<SizedWord> Convert(IEnumerable<WeightedWord> weightedWords)
        {
            var maxWeight = weightedWords.Max().Weight;
            var minWeight = weightedWords.Min().Weight;
            foreach (var word in weightedWords)
            {
                var font = new Font(fontSettings.FontFamily, GetFontSize(word.Weight, minWeight, maxWeight), fontSettings.FontStyle);
                var size = GetSize(word.Word, font);
                yield return new SizedWord(word.Word, font, size);
            }
        }

        private float GetFontSize(float currentSize, float minWeight, float maxWeight)
        {
            return currentSize > minWeight 
                ? (fontSettings.MaxFontSize * (currentSize - minWeight)) / (maxWeight - minWeight) : minWeight;
        }

        private Size GetSize(string text, Font font)
        {
            if (text.Length == 0)
                    throw new ArgumentException("text length should be grater than zero");
            return TextRenderer.MeasureText(text, font);
        }
    }
}