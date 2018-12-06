using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TagsCloudVisualization;

namespace TagsCloudContainer
{
    public class WordDrawInfoGetter
    {
        private readonly WordsContainer container;
        private readonly Func<TagCloudLayouter> layouterGenerator;
        private readonly FontSettings fontSettings;

        public WordDrawInfoGetter(WordsContainer container, Func<TagCloudLayouter> layouterGenerator, FontSettings fontSettings)
        {
            this.container = container;
            this.layouterGenerator = layouterGenerator;
            this.fontSettings = fontSettings;
        }

        public WordDrawInfo[] GetWordsAndRectangles()
        {
            if (container.ProcessedWords == null)
                throw new InvalidOperationException("You must process words at first");
            var info = new List<WordDrawInfo>();
            var layouter = layouterGenerator();
            container.WordsCenter = layouter.Center;
            foreach (var wordAndFrequency in container.ProcessedWords.GetOrderedWordsAndFrequencies())
            {
                var word = wordAndFrequency.Item1;
                var frequency = wordAndFrequency.Item2;
                var font = new Font(fontSettings.FontFamily, frequency * fontSettings.FontFactor);
                var size = TextRenderer.MeasureText(word, font);
                info.Add(new WordDrawInfo
                {
                    FontSize = frequency,
                    Rect = layouter.PutNextRectangle(size),
                    Word = word
                });
            }

            container.WordsToDraw = info.ToArray();
            return container.WordsToDraw;
        }
    }
}