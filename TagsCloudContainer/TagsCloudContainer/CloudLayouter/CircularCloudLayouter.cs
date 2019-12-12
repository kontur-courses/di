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
        private readonly IEnumerator<Point> spiralPoint;

        public CircularCloudLayouter(Point center ,Func<Point, ISpiral> spiralGenerator)
        {
            rectangles = new List<Rectangle>();
            spiralPoint = spiralGenerator(center).GetPoints().GetEnumerator();
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