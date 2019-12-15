using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloudGenerator.GeneratorCore.Extensions;

namespace TagCloudGenerator.GeneratorCore.CloudLayouters
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        private const double AzimuthDelta = Math.PI / 18;

        private readonly Size centralOffset;
        private readonly List<Rectangle> rectangles = new List<Rectangle>();
        private readonly ArchimedeanSpiral spiral = new ArchimedeanSpiral(AzimuthDelta);

        public CircularCloudLayouter(Point center) => centralOffset = new Size(center);

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.IsEmpty)
                throw new ArgumentException("Passed argument is empty.", nameof(rectangleSize));

            var newRectangle = FindNextRectangleOnTheSpiral(rectangleSize);

            if (rectangles.Count == 0)
            {
                var firstRectangleOffset = new Point(-rectangleSize.Width / 2,
                                                     -rectangleSize.Height / 2);
                newRectangle.Offset(firstRectangleOffset);
            }

            newRectangle = MoveRectangleToOrigin(newRectangle, rectangles);

            rectangles.Add(newRectangle);

            return newRectangle.CreateMovedCopy(centralOffset);
        }

        private Rectangle FindNextRectangleOnTheSpiral(Size rectangleSize)
        {
            foreach (var currentArchimedeanSpiralPoint in spiral.GetPoints())
            {
                var rectangle = new Rectangle(currentArchimedeanSpiralPoint, rectangleSize);

                if (!rectangle.IntersectsWith(rectangles))
                    return rectangle;

                if (currentArchimedeanSpiralPoint.X > 0)
                    continue;

                rectangle.Offset(new Point(-rectangleSize.Width, -rectangleSize.Height));

                if (!rectangle.IntersectsWith(rectangles))
                    return rectangle;
            }

            throw new Exception("Unreachable code, GetPoints() returns infinity lazy sequence.");
        }

        private static Rectangle MoveRectangleToOrigin(Rectangle rectangle, IReadOnlyCollection<Rectangle> rectangles)
        {
            if (rectangle.Contains(0, 0))
                return rectangle;

            var xDelta = -Math.Sign(rectangle.X + rectangle.Width / 2);
            var yDelta = -Math.Sign(rectangle.Y + rectangle.Height / 2);

            var sizes = new[] { new Size(xDelta, 0), new Size(0, yDelta) }.Where(size => !size.IsEmpty);

            foreach (var size in sizes)
                for (int i = 0; i < 2; i++)
                    while (true)
                    {
                        var movedRectangle = rectangle.CreateMovedCopy(size);
                        if (movedRectangle.IntersectsWith(rectangles) ||
                            XDistanceToCenter(movedRectangle) == 0 && size.Width != 0 ||
                            YDistanceToCenter(movedRectangle) == 0 && size.Height != 0)
                            break;
                        rectangle = movedRectangle;
                    }

            return rectangle;

            static int XDistanceToCenter(Rectangle rect) => rect.X + rect.Width / 2;
            static int YDistanceToCenter(Rectangle rect) => rect.Y + rect.Height / 2;
        }
    }
}