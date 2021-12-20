using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ResultProject;

namespace TagsCloudVisualization.Printing
{
    public class TextPrinter : IPrinter<Text>
    {
        private const int Margin = 50;
        private readonly IRectanglesReCalculator reCalculator;

        public TextPrinter(IRectanglesReCalculator reCalculator)
        {
            this.reCalculator = reCalculator;
        }
        
        public Result<Bitmap> GetBitmap(IColorScheme colorScheme, IEnumerable<Text> texts, Size? bitmapSize = null)
        {
            var fixedTexts = texts.ToList();
            if (!fixedTexts.Any()) return Result.Fail<Bitmap>($"text list is empty");

            return (bitmapSize is null
                    ? reCalculator.MoveToCenter(fixedTexts.Select(x => x.Rectangle).ToList())
                    : reCalculator.RecalculateRectangles(fixedTexts.Select(x => x.Rectangle).ToList(), new Size(bitmapSize.Value.Width - Margin, bitmapSize.Value.Height - Margin)))
                // .Then(x => bitmapSize is null
                //     ? reCalculator.MoveToCenter(x)
                //     : reCalculator.RecalculateRectangles(x, new Size(bitmapSize.Value.Width - Margin, bitmapSize.Value.Height - Margin)))
                // .Then(x => bitmapSize is null
                //     ? reCalculator.MoveToCenter(x)
                //     : reCalculator.RecalculateRectangles(x, new Size(bitmapSize.Value.Width - Margin, bitmapSize.Value.Height - Margin)))
                .Then(x => (x, bitmapSize ?? new Size(x.GetCircumscribedSize().Width, x.GetCircumscribedSize().Height)))
                .Then(x => (x.x, new Bitmap(x.Item2.Width, x.Item2.Height)))
                .Then(x => (x.x, x.Item2, Graphics.FromImage(x.Item2)))
                .Then(x => (x.x, x.Item2, Graphics.FromImage(x.Item2)))
                .Then(x =>
                {
                    var (rects, bmp, graphics) = x;
                    DrawText(graphics, fixedTexts.Select((y, i) => new Text(y.Word, y.Font, rects[i])), colorScheme);
                    // new RectanglePrinter(new RectanglesReCalculator()).DrawRectangles(colorScheme, graphics, rects);
                    return bmp;
                });
        }
        
        public Result<Bitmap> GetBitmap(IColorScheme colorScheme, Result<IEnumerable<Text>> objects, Size? bitmapSize = null)
        {
            return GetBitmap(colorScheme, objects.GetValueOrThrow()!, bitmapSize);
        }
        
        private void DrawText(Graphics graphics, IEnumerable<Text> texts, IColorScheme colorScheme)
        {
            foreach (var (word, font, rectangle) in texts)
            {
                var rf = new RectangleF(
                    rectangle.X - rectangle.Height / 6, 
                    rectangle.Y - rectangle.Height / 2, 
                    rectangle.Width * 1.4f, 
                    rectangle.Height == 0 ? 1 : rectangle.Height * 2);
                graphics.DrawString(word, 
                    new Font(font, rectangle.Height == 0 ? 1 : rectangle.Height, FontStyle.Regular), 
                    new SolidBrush(colorScheme.GetColorBy(rectangle.Size)), rf);
            }
        }
    }
}