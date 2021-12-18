using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ResultProject;

namespace TagsCloudVisualization.Printing
{
    public class RectanglePrinter : IPrinter<Rectangle>
    {
        private const int Margin = 100;
        private readonly IRectanglesReCalculator reCalculator;

        public RectanglePrinter(IRectanglesReCalculator reCalculator)
        {
            this.reCalculator = reCalculator;
        }
        
        public Result<Bitmap> GetBitmap(IColorScheme colorScheme, IEnumerable<Rectangle> objects, Size? bitmapSize = null)
        {
            var rects = objects.ToList();
            if (!rects.Any()) return Result.Fail<Bitmap>($"rectangle list is empty");
            return (bitmapSize is null
                ? reCalculator.MoveToCenter(rects)
                : reCalculator.RecalculateRectangles(rects, bitmapSize.Value))
                .Then(x => (x, new Bitmap(x.GetCircumscribedSize().Width + Margin / 2, x.GetCircumscribedSize().Height + Margin)))
                .Then(x => (x.x, x.Item2, Graphics.FromImage(x.Item2)))
                .Then(x =>
                {
                    var (rectangles, bitmap, graphics) = x;
                    DrawRectangles(colorScheme, graphics, rectangles);
                    return bitmap;
                });

            // var bmp = new Bitmap(recalculated.GetCircumscribedSize().Width + Margin / 2, recalculated.GetCircumscribedSize().Height + Margin);
            // using var graphics = Graphics.FromImage(bmp);
            // DrawRectangles(colorScheme, graphics, recalculated);

            // return bmp;
        }

        public Result<Bitmap> GetBitmap(IColorScheme colorScheme, Result<IEnumerable<Rectangle>> objects, Size? bitmapSize = null)
        {
            return GetBitmap(colorScheme, objects.GetValueOrThrow()!, bitmapSize);
        }

        private void DrawRectangles(IColorScheme colorScheme, Graphics graphics, IEnumerable<Rectangle> rectangles)
        {
            foreach (var rectangle in rectangles)
            {
                using var pen = new Pen(Brushes.Black, 1);
                pen.Color = colorScheme.GetColorBy(rectangle.Size);
                graphics.DrawRectangle(pen, rectangle);
            }
        }
    }
}