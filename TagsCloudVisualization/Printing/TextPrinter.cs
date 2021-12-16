using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization.Printing
{
    public class TextPrinter : IPrinter<Text>
    {
        private const int Margin = 0;
        private readonly IRectanglesReCalculator reCalculator;

        public TextPrinter(IRectanglesReCalculator reCalculator)
        {
            this.reCalculator = reCalculator;
        }
        
        public Bitmap GetBitmap(IColorScheme colorScheme, IEnumerable<Text> texts, Size? bitmapSize = null)
        {
            var fixedTexts = texts.ToList();
            if (!fixedTexts.Any()) throw new ArgumentException($"text list is empty");
            var recalculated = bitmapSize is null
                ? reCalculator.MoveToCenter(fixedTexts.Select(x => x.Rectangle).ToList())
                : reCalculator.RecalculateRectangles(fixedTexts.Select(x => x.Rectangle).ToList(), bitmapSize.Value);

            bitmapSize ??= new Size(recalculated.GetCircumscribedSize().Width + Margin / 2,
                recalculated.GetCircumscribedSize().Height + Margin);
            // var bmp = new Bitmap(bitmapSize.Value.Width, bitmapSize.Value.Height);
            var bmp = new Bitmap(recalculated.GetCircumscribedSize().Width, recalculated.GetCircumscribedSize().Height);

            using var graphics = Graphics.FromImage(bmp);
            DrawText(
                graphics, 
                fixedTexts.Select((x, i) => new Text(
                    x.Word, 
                    x.Font, 
                    recalculated[i])), colorScheme);

            var b = new RectanglePrinter(reCalculator).GetBitmap(colorScheme, recalculated);
            graphics.DrawImage(b, Point.Empty);
            return bmp;
        }
        
        private void DrawText(Graphics graphics, IEnumerable<Text> texts, IColorScheme colorScheme)
        {
            foreach (var (word, font, rectangle) in texts)
            {
                var rf = new RectangleF(
                    rectangle.X + Margin / 2, 
                    rectangle.Y - rectangle.Height / 2 + Margin / 2, 
                    rectangle.Width * 1.3f, 
                    rectangle.Height == 0 ? 1 : rectangle.Height * 2);
                graphics.DrawString(word, 
                    new Font(font, rectangle.Height == 0 ? 1 : rectangle.Height, FontStyle.Regular), 
                    new SolidBrush(colorScheme.GetColorBy(rectangle.Size)), rf);
            }
        }
    }
}