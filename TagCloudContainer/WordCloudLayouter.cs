using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.Linq;

namespace TagCloudContainer
{
    public class WordCloudLayouter
    {
        public CircularCloudLayouter RectangleLayouter;
        public Font LayouterFont;

        public WordCloudLayouter(CircularCloudLayouter rectangleLayouter, Font layouterFont)
        {
            RectangleLayouter = rectangleLayouter;
            LayouterFont = layouterFont;
        }

        public IEnumerable<(string word, Rectangle wordRectangle)> AddWords(
            IEnumerable<(string word, int occurrenceCount)> words)
        {
            return words.ToImmutableSortedSet()
                .Select(pair => (pair.word, rect: GetWordSize(pair.word, pair.occurrenceCount)))
                .Select(pair => (pair.word, RectangleLayouter.PutNextRectangle(pair.rect)));
        }

        public static Graphics GraphicsBase = Graphics.FromImage(new Bitmap(1, 1));

        public Size GetWordSize(string word, int occurrenceCount)
        {
            return (GraphicsBase.MeasureString(word, LayouterFont) * (MathF.Log(occurrenceCount) + 1f)).ToSize();
        }
    }
}