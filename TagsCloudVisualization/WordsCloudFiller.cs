using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class WordsCloudFiller
    {
        private readonly ICloudLayouter cloudLayouter;
        private readonly Font parentFont;

        public WordsCloudFiller(ICloudLayouter cloudLayouter, Font font)
        {
            this.cloudLayouter = cloudLayouter;
            parentFont = font;
        }

        public List<(string word, Rectangle rectangle, Font font)> GetRectanglesForWordsInCloud(Graphics g, List<string> words)
        {
            var font = parentFont;
            var rectangles = new List<(string word, Rectangle rectangle, Font font)>();

            var delta = (font.SizeInPoints / 2) / words.Count;

            foreach (var word in words)
            {
                font = new Font(font.Name, (font.SizeInPoints - delta));
                var size = g.MeasureString(word, font);
                var rec = cloudLayouter.PutNextRectangle(
                    new Size((int)Math.Ceiling(size.Width), (int)Math.Ceiling(size.Height)));

                rectangles.Add((word, rec, font));
            }

            return rectangles;
        }
    }
}