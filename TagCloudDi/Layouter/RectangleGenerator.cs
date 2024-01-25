using System.Drawing;
using TagCloudDi.TextProcessing;

namespace TagCloudDi.Layouter
{
    public class RectangleGenerator
    {
        public IReadOnlyList<(Rectangle rectangle, string word)> Rectangles { get; }

        public RectangleGenerator(TextProcessor textProcessor, Font font, CircularCloudLayouter layouter)
        {
            var allAmount = textProcessor.Words.Sum(x => x.Value);
            Rectangles = textProcessor
                .Words
                .OrderBy(x => x.Value)
                .Select(x => (layouter.PutNextRectangle(GetTextSize(x.Key, font)), x.Key))
                .ToList();
        }

        private Size GetTextSize(string text, Font font)
        {
            using var temporaryBitmap = new Bitmap(1, 1);
            using var temporaryGraphics = Graphics.FromImage(temporaryBitmap);
            return Size.Ceiling(temporaryGraphics.MeasureString(text, font));
        }
    }
}
