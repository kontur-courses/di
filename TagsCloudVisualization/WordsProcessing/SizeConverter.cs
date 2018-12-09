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
        private readonly IWeighter weighter;
        private float minWeight;
        private float maxWeight;
        
        public SizeConverter(IWeighter weighter, FontSettings fontSettings)
        {
            this.weighter = weighter;
            this.fontSettings = fontSettings;
        }
        
        
        public IEnumerable<SizedWord> Convert()
        {
            var weightedWords = weighter.WeightWords().ToList();
            maxWeight = weightedWords.Max().Weight;
            minWeight = weightedWords.Min().Weight;
            foreach (var word in weightedWords)
            {
                var font = new Font(fontSettings.FontFamily, GetFontSize(word.Weight), fontSettings.FontStyle);
                yield return new SizedWord(word.Word, font, GetSize(word.Word, font));
            }
        }

        private float GetFontSize(float currentSize)
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