using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.Settings;
using TagsCloudVisualization;

namespace TagsCloudContainer.Layout
{
    public interface ITagsCloudLayouter
    {
        CloudLayout GetCloudLayout(IEnumerable<string> words);
    }

    public class FontBasedLayouter : ITagsCloudLayouter
    {
        private readonly IFontFamilySettings fontFamilySettings;
        private readonly IFontSizeSelector fontSizeSelector;
        private readonly ICloudLayouter cloudLayouter;

        public FontBasedLayouter(IFontFamilySettings fontFamilySettings, IFontSizeSelector fontSizeSelector,
            ICloudLayouter cloudLayouter)
        {
            this.fontFamilySettings = fontFamilySettings ?? throw new ArgumentNullException(nameof(fontFamilySettings));
            this.fontSizeSelector = fontSizeSelector ?? throw new ArgumentNullException(nameof(fontSizeSelector));
            this.cloudLayouter = cloudLayouter ?? throw new ArgumentNullException(nameof(cloudLayouter));
        }

        public CloudLayout GetCloudLayout(IEnumerable<string> words)
        {
            if (words == null)
                throw new ArgumentNullException(nameof(words));

            var fontSizes = fontSizeSelector.GetFontSizes(words);
            var wordsLayout = GetWordsLayout(fontSizes.OrderByDescending(word => word.FontSize)).ToList();
            var rectangles = wordsLayout.Select(wordLayout => wordLayout.Rectangle).ToList();
            var wordsLocations = wordsLayout.Select(wordLayout => wordLayout.WordLayout);

            var size = GetOccupiedSize(rectangles);
            wordsLocations = OffsetLocation(wordsLocations.ToList());

            return new CloudLayout(wordsLocations, size);
        }

        private IEnumerable<(WordLayout WordLayout, Rectangle Rectangle)> GetWordsLayout(
            IEnumerable<(string, float)> fontSizes)
        {
            using var bitmap = new Bitmap(1, 1);
            using var graphics = Graphics.FromImage(bitmap);

            foreach (var (word, fontSize) in fontSizes)
            {
                var font = new Font(fontFamilySettings.FontFamily, fontSize);
                var wordSize = graphics.MeasureString(word, font, PointF.Empty, StringFormat.GenericTypographic)
                    .ToSize();

                var wordRectangle = cloudLayouter.PutNextRectangle(wordSize);
                yield return (new WordLayout(word, wordRectangle.Location, font), wordRectangle);
            }
        }

        private static Size GetOccupiedSize(IReadOnlyCollection<Rectangle> rectangles)
        {
            var topLeft = PointHelper.GetTopLeftCorner(rectangles.Select(rectangle => rectangle.Location));
            var bottomRight = PointHelper.GetBottomRightCorner(rectangles
                .Select(rectangle => new Point(rectangle.Right, rectangle.Bottom)));

            return new Size(bottomRight.X - topLeft.X, bottomRight.Y - topLeft.Y);
        }

        private static IEnumerable<WordLayout> OffsetLocation(IReadOnlyCollection<WordLayout> wordsLayouts)
        {
            var wordsPoints = wordsLayouts.Select(wordLocation => wordLocation.Location).ToList();
            var minX = wordsPoints.Select(point => point.X).Min();
            var minY = wordsPoints.Select(point => point.Y).Min();
            return wordsLayouts.Select(wordLocation =>
            {
                var location = wordLocation.Location;
                location.Offset(-minX, -minY);
                return new WordLayout(wordLocation.Word, location, wordLocation.Font);
            });
        }
    }
}