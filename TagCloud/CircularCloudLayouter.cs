using System.Collections.Generic;
using System.Drawing;

namespace TagCloud
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        internal readonly List<Rectangle> Rectangles = new List<Rectangle>();
        private readonly Spiral spiral;

        public CircularCloudLayouter(Point center)
        {
            spiral = new Spiral(center);
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            var newRect = FindPlaceForRect(rectangleSize);
            Rectangles.Add(newRect);
            return newRect;
        }

        private Rectangle FindPlaceForRect(Size rectangleSize)
        {
            var resultRect = new Rectangle(spiral.GetNextPoint(), rectangleSize);
            while (resultRect.IntersectsWith(Rectangles))
                resultRect = new Rectangle(spiral.GetNextPoint(), rectangleSize);

            return resultRect;
        }
    }
}