using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization;
using TagsCloudVisualizationTests.Interfaces;

namespace TagsCloudVisualizationTests.TestingLibrary
{
    public class PointsVisualizer : IVisualizer
    {
        private readonly List<Point> points;

        public PointsVisualizer(IEnumerable<Point> points)
        {
            this.points = points.ToList();
        }

        public void Draw(Graphics graphics)
        {
            var offset = PointHelper.GetTopLeftCorner(points);
            var brush = new SolidBrush(Color.Red);
            foreach (var point in points)
                graphics.FillRectangle(brush, point.X - offset.X, point.Y - offset.Y, 1, 1);
        }

        public Size GetBitmapSize()
        {
            if (points.Count == 0)
                return new Size(1, 1);

            var topLeft = PointHelper.GetTopLeftCorner(points);
            var bottomRight = PointHelper.GetBottomRightCorner(points);

            return new Size(
                bottomRight.X - topLeft.X + 1,
                bottomRight.Y - topLeft.Y + 1);
        }
    }
}