using System.Drawing;

namespace TagsCloud.Painter
{
    public class RectanglePainter : Painter<Rectangle>
    {
        public override void Paint(IEnumerable<Rectangle> figures, Image bitmap, Action colorChanger = null)
        {
#pragma warning disable CA1416
            var graphics = Graphics.FromImage(bitmap);
            graphics.TranslateTransform(bitmap.Width / 2f, bitmap.Height / 2f);

            foreach (var figure in figures)
            {
                colorChanger?.Invoke();
                graphics.FillRectangle(RectangleColor, figure);
#pragma warning restore CA1416
            }
        }

        public override Size GetBitmapSize(IEnumerable<Rectangle> figures)
        {
            var xMin = int.MaxValue;
            var xMax = int.MinValue;
            var yMin = int.MaxValue;
            var yMax = int.MinValue;

            foreach (var figure in figures)
            {
                xMin = figure.X < xMin ? figure.X : xMin;
                xMax = figure.X + figure.Width > xMax ? figure.X + figure.Width : xMax;
                yMin = figure.Y < yMin ? figure.Y : yMin;
                yMax = figure.Y + figure.Height > yMax ? figure.Y + figure.Height : yMax;
            }

            return new Size(xMax - xMin, yMax - yMin);
        }
    }
}