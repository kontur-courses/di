using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TagsCloudVisualization
{
    public class Sizer : ISizer
    {
        private readonly FontSettings fontSettings;
        private readonly IWeighter weighter;
        private float minWeight;
        private float maxWeight;
        
        public Sizer(IWeighter weighter, FontSettings fontSettings)
        {
            this.weighter = weighter;
            this.fontSettings = fontSettings;
        }
        
        
        public IEnumerable<SizedWord> SizeWords(IWordsProvider wordsProvider)
        {
            var weightedWords = weighter.WeightWords(wordsProvider).ToList();
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