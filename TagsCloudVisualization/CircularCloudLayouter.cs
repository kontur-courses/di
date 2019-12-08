using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter
    {
        public readonly Point Center;
        private readonly List<Rectangle> rectangles;
        private readonly ISpiral spiral;
        private readonly IEnumerator<Point> spiralPoints;

        public CircularCloudLayouter(Point center)
        {
            Center = center;
            rectangles = new List<Rectangle>();
            spiral = new ArchimedeanSpiral();
            spiralPoints = spiral.GetPoints().GetEnumerator();
        }

        public CircularCloudLayouter(Point center, ISpiral spiral)
        {
            Center = center;
            rectangles = new List<Rectangle>();
            this.spiral = spiral;
            spiralPoints = spiral.GetPoints().GetEnumerator();
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
                throw new ArgumentException(
                    $"Unable to create a rectangle with width = {rectangleSize.Width} and height = {rectangleSize.Height}");

            while(true)
            {
                var curentSpiralPoint = spiralPoints.Current;
                var curentPosition = new Point(
                    Center.X + curentSpiralPoint.X, 
                    Center.Y + curentSpiralPoint.Y);

                var rectangle = new Rectangle(curentPosition, rectangleSize);

                if (TryAddRectangle(rectangle)) return rectangle;
                spiralPoints.MoveNext();
            }
        }

        private bool TryAddRectangle(Rectangle rectangle)
        {
            if (rectangles.Exists(rec => rec.IntersectsWith(rectangle))) 
                return false;

            rectangles.Add(rectangle);
            return true;
        }

        internal IEnumerable<Rectangle> GetAllRectangles() => rectangles;
    }
}
