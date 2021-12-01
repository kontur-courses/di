using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Interfaces;

// Disable warning https://docs.microsoft.com/ru-ru/dotnet/fundamentals/code-analysis/quality-rules/ca1416
// as several methods use windows api
#pragma warning disable CA1416

namespace TagsCloudVisualization
{
    public class TagsCloudDrawer
    {
        private readonly Color _backgroundColor;
        private readonly IColorGenerator _colorGenerator;

        public TagsCloudDrawer(Color backgroundColor, IColorGenerator colorGenerator)
        {
            _backgroundColor = backgroundColor;
            _colorGenerator = colorGenerator ?? throw new ArgumentNullException(nameof(colorGenerator));
        }

        public void Draw(Bitmap bitmap, IEnumerable<Rectangle> rectangles)
        {
            if (bitmap == null) throw new ArgumentNullException(nameof(bitmap));
            using var graphics = Graphics.FromImage(bitmap);

            graphics.Clear(_backgroundColor);
            var shifted = GetShiftedRectangles(rectangles, Size.Truncate(bitmap.Size / 2f));
            FillWithRectangles(graphics, shifted, new RectangleF(Point.Empty, bitmap.Size));
        }

        private void FillWithRectangles(Graphics graphics, IEnumerable<Rectangle> rectangles, RectangleF bounds)
        {
            using var brush = new SolidBrush(new Color());
            foreach (var rectangle in rectangles)
            {
                if (!bounds.Contains(rectangle))
                    throw new Exception("Image cannot contain all rectangles");
                brush.Color = _colorGenerator.Generate();
                graphics.FillRectangle(brush, rectangle);
            }
        }

        private static IEnumerable<Rectangle> GetShiftedRectangles(IEnumerable<Rectangle> rectangles, Size vector)
        {
            return rectangles
                .Select(rectangle => new Rectangle(Point.Add(rectangle.Location, vector), rectangle.Size));
        }
    }
}
#pragma warning restore CA1416