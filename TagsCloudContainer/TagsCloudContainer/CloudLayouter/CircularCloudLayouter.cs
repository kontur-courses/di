using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.CloudLayouter.Spiral;

namespace TagsCloudContainer.CloudLayouter
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        private readonly List<Rectangle> rectangles;
        private IEnumerator<Point> spiralPoint;
        private readonly ISpiral spiral;

        public CircularCloudLayouter(ISpiral spiral)
        {
            rectangles = new List<Rectangle>();
            this.spiral = spiral;
        }

        public void SetCenter(Point center)
        {
            spiralPoint = spiral.GetPoints(center).GetEnumerator();
        }
        
        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            while (true)
            {
                spiralPoint.MoveNext();
                var point = spiralPoint.Current;
                var rectangle = new Rectangle(new Point(point.X - rectangleSize.Width / 2, point.Y - rectangleSize.Height / 2), rectangleSize);
                if (HasOverlappingRectangles(rectangle, rectangles)) continue;
                rectangles.Add(rectangle);
                return rectangle;
            }
        }
        
        
        public static bool HasOverlappingRectangles(Rectangle rectangle, IEnumerable<Rectangle> rectangles) =>
            rectangles.Any(r => r.IntersectsWith(rectangle));
        
        //различная реализация из-за сложностей алгоритмов 
        public static bool HasOverlappingRectangles(IEnumerable<Rectangle> rectangles) =>
             rectangles.Any(r => rectangles.Any(r1 => r != r1 && r.IntersectsWith(r1)));
    }
}