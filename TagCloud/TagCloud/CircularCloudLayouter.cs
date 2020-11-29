using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloud
{
    public class CircularCloudLayouter : ITagCloud
    {
        private readonly Point center;
        public List<Rectangle> Rectangles { get; }
        private readonly ISpiral spiral;

        public CircularCloudLayouter(Point center, double spiralDensity = 0.05)
        {
            this.center = center;
            Rectangles = new List<Rectangle>();
            spiral = new Spiral(center, spiralDensity);
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            while (true)
            {
                var currentPoint = spiral.CurrentPoint;
                var possibleRectangle = new Rectangle(currentPoint, rectangleSize);
                var canFit = Rectangles.All(rect => !rect.IntersectsWith(possibleRectangle));
                spiral.Next();
                if (!canFit) continue;
                Rectangles.Add(possibleRectangle);
                return possibleRectangle;
            }
        }
    }
}