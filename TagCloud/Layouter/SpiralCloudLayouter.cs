using System.Collections.Generic;
using TagCloud.Interfaces;

namespace TagCloud.Layouter
{
    public class SpiralCloudLayouter : ICloudLayouter
    {
        private const double SpiralAngleInterval = 0.1;
        private const double SpiralTurnsInterval = 0.1;

        private readonly Point origin;
        private readonly List<Rectangle> rectanglesList;
        private readonly ISpiral spiral;
        private double currentSpiralAngle;

        public SpiralCloudLayouter(ISpiral spiral, Point origin)
        {
            this.spiral = spiral;
            this.origin = origin;
            rectanglesList = new List<Rectangle>();
        }

        public IReadOnlyCollection<Rectangle> GetCloud()
        {
            return rectanglesList;
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            var rectangle = PutOnSpiral(rectangleSize);
            if (rectangle == null)
                return null;
            rectangle = MakeCloserToCenter(rectangle);
            rectanglesList.Add(rectangle);
            return rectangle;
        }

        private Rectangle MakeCloserToCenter(Rectangle rectangle)
        {
            var directionToCenter = new Vector(rectangle.Center, origin).Normalized();
            var currentDirection = directionToCenter;
            var previousPosition = new Point(0, 0);
            while (directionToCenter.IsSameDirection(currentDirection)
                   && !rectangle.IsIntersectsWithAnyRect(rectanglesList))
            {
                previousPosition = rectangle.Center;
                rectangle.Center += directionToCenter;
                currentDirection = new Vector(rectangle.Center, origin).Normalized();
            }

            rectangle.Center = previousPosition;
            return rectangle;
        }

        private Rectangle PutOnSpiral(Size rectangleSize)
        {
            var newRectangle = new Rectangle(origin, rectangleSize);
            while (newRectangle.IsIntersectsWithAnyRect(rectanglesList))
            {
                currentSpiralAngle += SpiralAngleInterval;
                var rectCenter = spiral.Put(origin, currentSpiralAngle, SpiralTurnsInterval);
                newRectangle.Center = rectCenter;
            }

            return newRectangle;
        }
    }
}