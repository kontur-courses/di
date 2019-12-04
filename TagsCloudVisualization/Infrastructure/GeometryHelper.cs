using System;
using System.Drawing;

namespace TagsCloudVisualization.Infrastructure
{
    public class GeometryHelper
    {
        public static Rectangle GetRectangleFromCenterPoint(Point center, Size size)
        {
            return new Rectangle(center.X - size.Width / 2, center.Y - size.Height / 2, size.Width, size.Height);
        }

        public static Point[] GetRectangleNodes(Rectangle rectangle)
        {
            return new[]
            {
                rectangle.Location, new Point(rectangle.Left, rectangle.Bottom),
                new Point(rectangle.Right, rectangle.Top), new Point(rectangle.Right, rectangle.Bottom),
            };
        }

        public static double GetDistanceBetweenPoints(Point point1, Point point2)
        {
            return Math.Sqrt(Math.Pow((point2.X - point1.X), 2) + Math.Pow((point2.Y - point1.Y), 2));
        }

        public static double GetDistanceBetweenRectanglesCenters(Rectangle rectangle1, Rectangle rectangle2)
        {
            return GetDistanceBetweenPoints(
                new Point(rectangle1.X + rectangle1.Width / 2, rectangle1.Y + rectangle1.Height / 2),
                new Point(rectangle2.X + rectangle2.Width / 2, rectangle2.Y + rectangle2.Height / 2));
        }
    }
}