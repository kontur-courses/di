using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloud
{
    public class TextRectangle
    {
        private Rectangle rectangle;

        public Rectangle Rectangle => rectangle;

        public string Text { get; }

        public TextRectangle(Point location, Size size, string text)
        {
            rectangle = new Rectangle(location, size);
            Text = text;
        }

        public void SetLocation(Point newLocation) => rectangle.Location = newLocation;

        public void ChangeSize(double widthCoeff, double heightCoeff)
        {
            rectangle.X = (int) (rectangle.X * widthCoeff);
            rectangle.Y = (int) (rectangle.Y * heightCoeff);
            rectangle.Width = (int)(rectangle.Width * widthCoeff);
            rectangle.Height = (int)(rectangle.Height * heightCoeff);
        }

        public static IEnumerable<TextRectangle> NormalizeRectangles(IEnumerable<TextRectangle> rectangles, Size size)
        {
            var movedRectangles = MoveToFourthQuarter(rectangles);
            var heightCoeff = size.Height * 1.0 / movedRectangles.Select(tr => tr.Rectangle).Max(r => r.Bottom);
            var widthCoeff = size.Width * 1.0 / movedRectangles.Select(tr => tr.Rectangle).Max(r => r.Right);
            return ChangeSize(movedRectangles, widthCoeff, heightCoeff);
        }

        private static IEnumerable<TextRectangle> MoveToFourthQuarter(IEnumerable<TextRectangle> rectangles)
        {
            var left = rectangles.Select(tr => tr.Rectangle).Min(r => r.Left);
            var top = rectangles.Select(tr => tr.Rectangle).Min(r => r.Top);

            return rectangles.Select(tr =>
            {
                tr.SetLocation(new Point(tr.Rectangle.X - left, tr.Rectangle.Y - top));
                return tr;
            }).ToArray();
        }

        private static IEnumerable<TextRectangle> ChangeSize(IEnumerable<TextRectangle> rectangles, double widthCoeff, double heightCoeff)
        {
            return rectangles.Select(tr =>
            {
                tr.ChangeSize(widthCoeff, heightCoeff);
                return tr;
            }).ToArray();

        }
    }
}