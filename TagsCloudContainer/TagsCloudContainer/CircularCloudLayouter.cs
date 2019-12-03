﻿using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter
    {
        private List<Rectangle> rectangles = new List<Rectangle>();
        private IEnumerator<Point> spiralPoint;

        public CircularCloudLayouter(Point center)
        {
            var spiral = new Spiral(center);
            spiralPoint = spiral.GetPoints().GetEnumerator();
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