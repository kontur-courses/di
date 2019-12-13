using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudApp.LayOuter
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        private List<Rectangle> rectanglesList;
        private Point сenter;

        public CircularCloudLayouter()
        {
            var currentCenter = new Point(1000, 1000);
            if (currentCenter.X < 0 || currentCenter.Y < 0)
                throw new ArgumentException("Center coordinates should be greater than null");
            rectanglesList = new List<Rectangle>();
            сenter = currentCenter;
        }

        public CircularCloudLayouter(Point center)
        {
            var currentCenter = center;
            if (currentCenter.X < 0 || currentCenter.Y < 0)
                throw new ArgumentException("Center coordinates should be greater than null");
            rectanglesList = new List<Rectangle>();
            сenter = currentCenter;
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectanglesList.Count == 0)
            {
                var newCoordinates = new Point(сenter.X - (rectangleSize.Width / 2), сenter.Y - (rectangleSize.Height / 2));
                rectanglesList.Add(new Rectangle(newCoordinates, rectangleSize));
                return new Rectangle(newCoordinates, rectangleSize);
            }
            var rectangle = new Rectangle(сenter, rectangleSize);
            var suitablePointsList = GetSuitablePointsList(rectangle);
            rectangle.Location = suitablePointsList.OrderBy(point => GetSquaredDistanceFromCenterToRectangle(point)).First();
            rectanglesList.Add(rectangle);
            return rectanglesList[rectanglesList.Count - 1];
        }

        private bool CheckRectangleDoesNotIntersectWithAnyAnother(Rectangle rectangle)
        {
            return !rectanglesList.Any(t => t.IntersectsWith(rectangle));
        }

        private List<Point> GetSuitablePointsList(Rectangle newRectangle)
        {
            var pointsList = new List<Point>();
            foreach (var rectangle in rectanglesList)
            {
                var startLocations = GetStartLocations(rectangle, newRectangle);
                for (var j = 0; j < startLocations.Count; j++)
                {
                    newRectangle.Location = startLocations[j];
                    var side = (j == 0 || j == 1) ? rectangle.Height : rectangle.Width;
                    for (var k = 0; k < side; k = k + (side / 4))
                    {
                        if (j == 0 || j == 1)
                            newRectangle.Y -= k;
                        else
                            newRectangle.X += k;
                        if (CheckRectangleDoesNotIntersectWithAnyAnother(newRectangle))
                            pointsList.Add(newRectangle.Location);
                    }
                }
            }
            return pointsList;
        }

        private static List<Point> GetStartLocations(Rectangle rectangle, Rectangle newRectangle)
        {
            var rectangleSides = new List<Point>
            {
                new Point(rectangle.Left - newRectangle.Width, rectangle.Top),
                new Point(rectangle.Right, rectangle.Top),
                new Point(rectangle.Left, rectangle.Top - newRectangle.Height),
                new Point(rectangle.Left, rectangle.Bottom)
            };
            return rectangleSides;
        }

        private int GetSquaredDistanceFromCenterToRectangle(Point point)
        {
            return (int)(Math.Pow(сenter.X - point.X, 2) + Math.Pow(сenter.Y - point.Y, 2));
        }
    }
}
