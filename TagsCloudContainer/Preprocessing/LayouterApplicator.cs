using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TagsCloudContainer.Settings;
using TagsCloudContainer.Layouter;

namespace TagsCloudContainer.Preprocessing
{
    public class LayouterApplicator
    {
        private readonly Func<TagCloudLayouter> layouterGenerator;
        private readonly FontSettings fontSettings;
        
        public Point WordsCenter { get; private set; }

        public LayouterApplicator(Func<TagCloudLayouter> layouterGenerator, FontSettings fontSettings)
        {
            this.layouterGenerator = layouterGenerator;
            this.fontSettings = fontSettings;
        }

        public IEnumerable<WordInfo> GetWordsAndRectangles(IEnumerable<WordInfo> wordsAndFrequencies)
        {
            if (wordsAndFrequencies == null)
                throw new InvalidOperationException("You must process words at first");
            var layouter = layouterGenerator();
            WordsCenter = layouter.Center;
            foreach (var wordAndFrequency in wordsAndFrequencies)
            {
                var word = wordAndFrequency.Word;
                var frequency = wordAndFrequency.Frequency;
                var font = new Font(fontSettings.FontFamily, frequency * fontSettings.FontSizeFactor);
                var size = TextRenderer.MeasureText(word, font);
                yield return new WordInfo
                {
                    FontSize = frequency,
                    Rect = layouter.PutNextRectangle(size),
                    Word = word
                };
            }
        }
    }
}