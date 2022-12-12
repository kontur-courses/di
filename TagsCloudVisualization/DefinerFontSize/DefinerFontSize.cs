using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Infrastructure.Analyzer;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.DefinerFontSize
{
    public class DefinerFontSize : IDefinerFontSize
    {
        private readonly FontSettings fontSettings;

        public DefinerFontSize(FontSettings fontSettings)
        {
            this.fontSettings = fontSettings;
        }

        public IEnumerable<WordWithFont> DefineFontSize(IEnumerable<IWeightedWord> words)
        {
            var weightedWords = words.ToArray();

            if (weightedWords.Length == 0) yield break;

            var countWord = weightedWords.Max(p => p.Weight);
            var difference = fontSettings.MaxEmSize - fontSettings.MinEmSize;

            foreach (var word in weightedWords)
            {
                var percent = word.Weight / (float)countWord;
                var emSize = fontSettings.MinEmSize + difference * percent;
                yield return new WordWithFont
                {
                    Font = new Font(fontSettings.FontFamily, emSize, fontSettings.Style),
                    Word = word.Word
                };
            }
        }
    }
}