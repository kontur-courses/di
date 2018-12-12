using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class WordsCloudFiller
    {
        private readonly ICloudLayouter cloudLayouter;
        private readonly Font parentFont;
        private const float Coefficient = 5;

        public WordsCloudFiller(ICloudLayouter cloudLayouter, Font font)
        {
            this.cloudLayouter = cloudLayouter;
            parentFont = font;
        }

        public List<(string word, Rectangle rectangle, Font font)> GetRectanglesForWordsInCloud(
            Graphics g,
            List<(string Word, int CountInText)> words)
        {
            var maxFrequency = words[0].Item2;

            var maxFontSize = parentFont.Size;
            var minFontSize = maxFontSize / Coefficient;

            var font = parentFont;

            var rectangles = new List<(string word, Rectangle rectangle, Font font)>();

            foreach (var word in words)
            {
                font = new Font(font.Name,
                    (minFontSize + (maxFontSize - minFontSize) * ((float) word.CountInText / maxFrequency)));
                var size = g.MeasureString(word.Word, font);
                var rec = cloudLayouter.PutNextRectangle(
                    new Size((int) Math.Ceiling(size.Width), (int) Math.Ceiling(size.Height)));

                rectangles.Add((word.Word, rec, font));
            }

            return rectangles;
        }
    }
}