using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter
    {
        private const double SpiralStepCoefficient = 1;
        private const double RadiusStep = 0.001d;

        private readonly Point center;
        private readonly List<Rectangle> rectangles = new List<Rectangle>();

        public List<Rectangle> Rectangles => rectangles.Select(r => new Rectangle(r.Location, r.Size)).ToList();

        public CircularCloudLayouter(Point center)
        {
            this.center = center;
        }

        public Rectangle PutNextRectangle(Size size)
        {
            var rectangle = GenerateSuitableRectangle(size);
            rectangles.Add(rectangle);
            return rectangle;
        }

        private Rectangle GenerateSuitableRectangle(Size size)
        {
            var rectangle = new Rectangle(center, size);

            rectangle.X -= size.Width / 2;
            rectangle.Y -= size.Height / 2;

            if (rectangles.Any())
                rectangle = FindSuitablePlaceFor(rectangle);

            return rectangle;
        }

        private Rectangle FindSuitablePlaceFor(Rectangle rectangle)
        {
            var placedRectangle = SpiralPath.GetSpiralPoints(SpiralStepCoefficient, RadiusStep)
                .Select(p => rectangle.Move(p))
                .First(r => !IntersectsWithExistingRectangles(r));

            return placedRectangle;
        }

        private bool IntersectsWithExistingRectangles(Rectangle rectangle)
        {
            return rectangles.Any(r => r.IntersectsWith(rectangle));
        }
    }
}
