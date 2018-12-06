using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TagsCloudContainer.Settings;
using TagsCloudContainer.Layouter;
using TagsCloudContainer.Painter;

namespace TagsCloudContainer.Preprocessing
{
    public class DrawInfoGetter
    {
        private readonly Func<TagCloudLayouter> layouterGenerator;
        private readonly FontSettings fontSettings;
        
        public Point WordsCenter { get; private set; }

        public DrawInfoGetter(Func<TagCloudLayouter> layouterGenerator, FontSettings fontSettings)
        {
            this.layouterGenerator = layouterGenerator;
            this.fontSettings = fontSettings;
        }

        public WordDrawInfo[] GetWordsAndRectangles(WordsManager processedWords)
        {
            if (processedWords == null)
                throw new InvalidOperationException("You must process words at first");
            var info = new List<WordDrawInfo>();
            var layouter = layouterGenerator();
            WordsCenter = layouter.Center;
            foreach (var wordAndFrequency in processedWords.GetOrderedWordsAndFrequencies())
            {
                var word = wordAndFrequency.Item1;
                var frequency = wordAndFrequency.Item2;
                var font = new Font(fontSettings.FontFamily, frequency * fontSettings.FontSizeFactor);
                var size = TextRenderer.MeasureText(word, font);
                info.Add(new WordDrawInfo
                {
                    FontSize = frequency,
                    Rect = layouter.PutNextRectangle(size),
                    Word = word
                });
            }

            return info.ToArray();
        }
    }
}