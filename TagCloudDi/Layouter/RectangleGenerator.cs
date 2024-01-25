using System.Drawing;
using TagCloudDi.TextProcessing;

namespace TagCloudDi.Layouter
{
    public class RectangleGenerator(TextProcessor textProcessor, Settings settings, CircularCloudLayouter layouter)
    {
        public IEnumerable<(Rectangle rectangle, string word, float fontSize)> GetRectanglesData()
        {
            var frequences = textProcessor.GetWordsFrequency();
            var totalAmount = frequences.Sum(x => x.Value);
            return frequences
                .OrderByDescending(x => x.Value)
                .Select(x =>
                {
                    using var font = new Font(settings.FontName, settings.FontSize * (
                            x.Value * 100 / totalAmount), FontStyle.Regular);
                    return (layouter.PutNextRectangle(GetTextSize(x.Key, font)), x.Key, font.Size);
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
