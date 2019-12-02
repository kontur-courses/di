using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter
    {
        public readonly Point Center;

        private readonly List<Rectangle> rectangles;
        private readonly ISpiralGenerator spiral;

        public CircularCloudLayouter(Point center, ISpiralGenerator generator)
        {
            Center = center;
            rectangles = new List<Rectangle>();
            spiral = generator;
        }

        public CircularCloudLayouter(Point center) : 
            this(center, new ArchimedeanSpiral(center, 0.5 / (2 * Math.PI)))
        { }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if(rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
                throw new ArgumentException("Size of rectangle can't be less than zero.");

            while (true)
            {
                var rectVariant = new Rectangle(spiral.GetNextSpiralPoint(), rectangleSize);
                
                if (rectangles.Any(rectVariant.IntersectsWith)) continue;
                
                rectangles.Add(rectVariant);
                return rectVariant;
            }
        }

        public IReadOnlyList<Rectangle> GetRectangles() => rectangles;
    }
}