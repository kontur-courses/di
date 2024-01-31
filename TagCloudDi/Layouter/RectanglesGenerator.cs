using System.Drawing;
using TagCloudDi.TextProcessing;

namespace TagCloudDi.Layouter
{
    public class RectanglesGenerator(ITextProcessor textProcessor, Settings settings, ILayouter layouter) : IRectanglesGenerator
    {
        public IEnumerable<RectangleData> GetRectanglesData()
        {
            var frequencies = textProcessor.GetWordsFrequency();
            var totalAmount = frequencies.Sum(x => x.Value);
            return frequencies
                .OrderByDescending(x => x.Value)
                .Select(x =>
                {
                    using var font = new Font(settings.FontName, settings.FontSize * (
                        x.Value * 100 / totalAmount), FontStyle.Regular);
                    return new RectangleData(layouter.PutNextRectangle(GetTextSize(x.Key, font)), x.Key, font.Size);
                })
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
