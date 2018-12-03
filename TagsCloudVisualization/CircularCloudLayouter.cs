using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter
    {
        private const double SpiralAngleInterval = 0.1;
        private const double SpiralTurnsInterval = 0.5;

        private Point origin;
        private List<Rectangle> rectanglesList;
        public int Width { get; private set; }
        public int Height { get; private set; }
        private double currentSpiralAngle;

        public CircularCloudLayouter(Point origin)
        {
            this.origin = origin;
            rectanglesList = new List<Rectangle>();
        }

        public IReadOnlyCollection<Rectangle> GetCloud()
            => rectanglesList;

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            var rectangle = PutOnSpiral(rectangleSize);
            if (rectangle == null)
                return null;
            rectangle = MakeCloserToCenter(rectangle);
            rectanglesList.Add(rectangle);
            UpdateWidthAndHeight(rectangle);
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

        private void UpdateWidthAndHeight(Rectangle rectangle)
        {
            var newWidth = Math.Max(Math.Abs(rectangle.RightXCoord), Math.Abs(rectangle.LeftXCoord));
            var newHeight = Math.Max(Math.Abs(rectangle.TopYCoord), Math.Abs(rectangle.BottomYCoord));
            if (newWidth > Width)
                Width = (int)newWidth;
            if (newHeight > Height)
                Height = (int)newHeight;
        }

        private Rectangle PutOnSpiral(Size rectangleSize)
        {
            var newRectangle = new Rectangle(origin, origin, rectangleSize);
            while (newRectangle.IsIntersectsWithAnyRect(rectanglesList))
            {
                currentSpiralAngle += SpiralAngleInterval;
                var rectCenter = ArithmeticSpiral(currentSpiralAngle, SpiralTurnsInterval);
                newRectangle.Center = rectCenter;
            }

            return newRectangle;
        }

        private Point ArithmeticSpiral(double angle, double turnsInterval)
        {
            var x = origin.X + (turnsInterval * angle) * Math.Cos(angle);
            var y = origin.Y + (turnsInterval * angle) * Math.Sin(angle);

            return new Point(x, y);
        }
    }
}
