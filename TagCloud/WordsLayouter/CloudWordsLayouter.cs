using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TagCloud.Data;
using TagCloud.RectanglesLayouter;

namespace TagCloud.WordsLayouter
{
    public class CloudWordsLayouter : IWordsLayouter
    {
        private readonly IRectangleLayouter rectangleLayouter;

        public static readonly HashSet<string> Fonts =
            new HashSet<string>(FontFamily.Families.Select(font => font.Name));

        public CloudWordsLayouter(IRectangleLayouter rectangleLayouter)
        {
            this.rectangleLayouter = rectangleLayouter;
        }

        public IEnumerable<WordImageInfo> GenerateLayout(IEnumerable<WordInfo> words, FontFamily fontFamily, int fontMultiplier)
        {
            foreach (var wordInfo in words)
            {
                var font = new Font(fontFamily, wordInfo.Occurrences * fontMultiplier);
                var size = TextRenderer.MeasureText(wordInfo.Word, font);
                var rectangle = rectangleLayouter.PutNextRectangle(size);
                yield return new WordImageInfo(wordInfo.Word, font, rectangle, wordInfo.Frequency);
            }
        }
    }
}