using System.Collections.Generic;
using System.Drawing;

namespace TagCloudTask.Visualization
{
    public class Drawer : IDrawer
    {
        private const int LineWidth = 2;

        private readonly List<Color> colors = new List<Color>
        {
            Color.Green,
            Color.Red,
            Color.CadetBlue,
            Color.Orange,
            Color.DeepPink,
            Color.Black,
            Color.Chartreuse
        };

        public void DrawCanvasBoundary(Graphics g, Size imgSize)
        {
            var boundary = new Rectangle(Point.Empty,
                new Size(imgSize.Width - 1, imgSize.Height - 1));

            using (var pen = new Pen(Brushes.Red, LineWidth))
            {
                g.DrawRectangle(pen, boundary);
            }
        }

        public void DrawAxis(Graphics g, Size imgSize, Point cloudCenter)
        {
            using (var pen = new Pen(Brushes.Black, LineWidth))
            {
                g.DrawLine(pen, cloudCenter, new Point(cloudCenter.X, 0));
                g.DrawLine(pen, cloudCenter, new Point(cloudCenter.X, imgSize.Height));

                g.DrawLine(pen, cloudCenter, new Point(0, cloudCenter.Y));
                g.DrawLine(pen, cloudCenter, new Point(imgSize.Width, cloudCenter.Y));
            }
        }

        public void DrawCloudBoundary(Graphics g, Size imgSize, Point cloudCenter, int cloudCircleRadius)
        {
            var location = new Point(
                cloudCenter.X - cloudCircleRadius,
                cloudCenter.Y - cloudCircleRadius);

            var size = new Size(cloudCircleRadius * 2, cloudCircleRadius * 2);

            using (var pen = new Pen(Brushes.DodgerBlue, LineWidth))
            {
                g.DrawEllipse(pen, new Rectangle(location, size));
            }
        }

        public void DrawRectangles(Graphics g, List<Rectangle> rectangles)
        {
            var i = 0;
            foreach (var rectangle in rectangles)
            {
                g.DrawRectangle(Pens.Black, rectangle);

                var nextColor = colors[i % colors.Count];
                g.FillRectangle(new SolidBrush(nextColor), rectangle);

                i++;
            }
        }
    }
}