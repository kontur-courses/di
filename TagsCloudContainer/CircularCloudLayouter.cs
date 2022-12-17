using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        private CircularCloudSettingsProvider settings;

        public CircularCloudLayouter(CircularCloudSettingsProvider settings)
        {
            this.settings = settings;
        }
        public List<Rectangle> GenerateCloud(Point center, List<Size> rectangleSizes)
        {
            var requestedList = new List<Rectangle>();
            foreach (var rectangleSize in rectangleSizes)
            {
                requestedList.Add(GetNextRectangle(center, requestedList, rectangleSize));
            }

            return requestedList;
        }

        public Rectangle GetNextRectangle(Point center, List<Rectangle> rectangles, Size rectangleSize)
        {
            if (rectangleSize.Height <= 0 || rectangleSize.Width <= 0)
                throw new ArgumentException("Height and Width must be positive");
            Rectangle newRectangle;
            if (!GetNewRectanglePostition(center, rectangles, rectangleSize, out newRectangle))
                throw new ArgumentException("Rectangle size so much");
            return newRectangle;
        }

        private bool GetNewRectanglePostition(Point center, List<Rectangle> rectangles, Size rectangleSize, out Rectangle newRectangle)
        {
            var x = 0;
            var y = 0;
            double angle = 0;
            var rectPos = new Point(x - rectangleSize.Width / 2 + center.X,
                y - rectangleSize.Height / 2 + center.Y);
            var rectangle = new Rectangle(rectPos, rectangleSize);
            newRectangle = rectangle;
            int stepSize = settings.CircularCloudSettings.StepSize;
            while (true)
            {
                newRectangle = rectangle;
                if (rectangles.Count == 0) return true;
                var isContain = rectangles.Any(a => a.Contains(rectangle.GetCenter()));
                var isIntersect = rectangles.Any(a => a.IntersectsWith(rectangle));
                if (!isIntersect && !isContain)
                {
                    newRectangle = rectangle;
                    return true;
                }

                angle += Math.PI / 180;
                x = (int)(angle * Math.Cos(angle) * stepSize);
                y = (int)(angle * Math.Sin(angle) * stepSize);
                rectPos = new Point(x - rectangleSize.Width / 2 + center.X,
                    y - rectangleSize.Height / 2 + center.Y);
                rectangle = new Rectangle(rectPos, rectangleSize);
            }
        }
    }
}