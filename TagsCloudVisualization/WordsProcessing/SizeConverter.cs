using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TagsCloudVisualization.Infrastructure;

namespace TagsCloudVisualization.WordsProcessing
{
    public class SizeConverter : ISizeConverter
    {
        private readonly FontSettings fontSettings;
        
        public SizeConverter(FontSettings fontSettings)
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
                yield return new SizedWord(word.Word, font, GetSize(word.Word, font));
            }
        }

        private float GetFontSize(float currentSize, float minWeight, float maxWeight)
        {
            return currentSize > minWeight 
                ? (fontSettings.MaxFontSize * (currentSize - minWeight)) / (maxWeight - minWeight) : 1;
        }

        private Size GetSize(string text, Font font)
        {
            return TextRenderer.MeasureText(text, font);
        }
    }
}