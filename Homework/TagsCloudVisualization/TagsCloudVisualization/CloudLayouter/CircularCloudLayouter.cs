using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using TagsCloudVisualization.CloudLayouter;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        private readonly SpiralInfo spiralInfo;
        private readonly List<Rectangle> rectangles;
        public List<Rectangle> Rectangles => new List<Rectangle>(rectangles);

        public CircularCloudLayouter(Point center, double radiusStep = 0.00001, double angleStep = 0.01)
        {
            rectangles = new List<Rectangle>();
            spiralInfo = new SpiralInfo(radiusStep, angleStep, center);
        }

        public CircularCloudLayouter(SpiralInfo spiralInfo)
        {
            this.spiralInfo = spiralInfo;
        }

        public Rectangle PutNextRectangle(Size rectangleSize, Point? position = null)
        {
            if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
                throw new ArgumentException("Width and height should be integer positive numbers");

            var newPosition = position ?? GetAppropriatePlace(rectangleSize);
            var newRectangle = new Rectangle(newPosition, rectangleSize);

            rectangles.Add(newRectangle);

            return newRectangle;
        }

        public void ReplaceRectangles()
        {
            var rectanglesCopy = new List<Rectangle>(rectangles);
            rectangles.Clear();

            rectanglesCopy
                .OrderBy(rectangle => rectangle.Width * rectangle.Height)
                .ToList()
                .ForEach(rectangle => rectangles
                    .Add(new Rectangle(GetAppropriatePlace(rectangle.Size), rectangle.Size)));
        }

        private Point GetAppropriatePlace(Size rectangleSize)
        {
            if (rectangles.Count == 0)
                return spiralInfo.Center;

            var (currentPosition, currentAngle, currentRadius) = (spiralInfo.Center, 0.0, 0.0);
            var potentialRectangle = new Rectangle(currentPosition, rectangleSize);

            while (!CanBePlaced(potentialRectangle))
            {
                (currentPosition, currentAngle, currentRadius) = NextStep(
                    currentPosition, currentAngle, currentRadius);
                if (!potentialRectangle.Location.Equals(currentPosition))
                    potentialRectangle = new Rectangle(currentPosition, rectangleSize);
            }
            return currentPosition;
        }

        private (Point, double, double) NextStep(
            Point currentPosition, double currentAngle, double currentRadius)
        {
            var sin = Math.Sin(currentAngle);
            var cos = Math.Cos(currentAngle);
            currentPosition.X = (int)(spiralInfo.Center.X - currentRadius * currentAngle * sin);
            currentPosition.Y = (int)(spiralInfo.Center.Y + currentRadius * currentAngle * cos);
            currentAngle = (currentAngle + spiralInfo.AngleStep) % SpiralInfo.MaxAngle;
            currentRadius += spiralInfo.RadiusStep;

            return (currentPosition, currentAngle, currentRadius);
        }

        private bool CanBePlaced(Rectangle rectangle)
        {
            return !HaveIntersectionWithAnotherRectangle(rectangle) &&
                   !IsPlacedInAnotherRectangle(rectangle);
        }

        private bool HaveIntersectionWithAnotherRectangle(Rectangle rectangle)
        {
            return rectangles.Any(anotherRectangle => RectanglesChecker.HaveIntersection(rectangle, anotherRectangle));
        }

        private bool IsPlacedInAnotherRectangle(Rectangle rectangle)
        {
            return rectangles.Any(anotherRectangle => RectanglesChecker.IsNestedRectangle(rectangle, anotherRectangle) ||
                                                      RectanglesChecker.IsNestedRectangle(anotherRectangle, rectangle));
        }
    }
}
