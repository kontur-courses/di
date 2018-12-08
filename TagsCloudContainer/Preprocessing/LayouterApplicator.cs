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
        private readonly Func<ITagCloudLayouter> layouterGenerator;
        private readonly FontSettings fontSettings;
        
        public Point WordsCenter { get; private set; }

        public LayouterApplicator(Func<ITagCloudLayouter> layouterGenerator, FontSettings fontSettings)
        {
            this.layouterGenerator = layouterGenerator;
            this.fontSettings = fontSettings;
        }

        public IEnumerable<WordInfo> GetWordsAndRectangles(IEnumerable<WordInfo> wordsAndFrequencies)
        {
            if (wordsAndFrequencies == null)
                throw new ArgumentNullException(nameof(wordsAndFrequencies));
            var layouter = layouterGenerator();
            WordsCenter = layouter.Center;
            foreach (var wordAndFrequency in wordsAndFrequencies)
            {
                var word = wordAndFrequency.Word;
                var frequency = wordAndFrequency.Frequency;
                var font = new Font(fontSettings.Font.FontFamily, frequency * fontSettings.FontSizeFactor);
                var size = TextRenderer.MeasureText(word, font);
                wordAndFrequency.FontSize = font.Size;
                wordAndFrequency.Rect = layouter.PutNextRectangle(size);
                yield return wordAndFrequency;
            }
        }
    }
}