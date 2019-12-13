using System.Collections.Generic;
using System.Drawing;
using TagsCloud.Extensions;
using TagsCloud.Interfaces;
using System.Linq;
using System;

namespace TagsCloud.CloudLayouters
{
    public class MiddleRectangleCloudLayouter : ITagCloudLayouter
    {
        private readonly List<Rectangle> previousRectangles;
        private readonly SortedList<double, List<(Point point, int countNear)>> pointForAdd;
        private readonly Point center;

        public MiddleRectangleCloudLayouter()
        {
            center = new Point(0, 0);
            previousRectangles = new List<Rectangle>();
            pointForAdd = new SortedList<double, List<(Point point, int countNear)>>();
            pointForAdd.Add(0, new List<(Point point, int countNear)>() { (new Point(0, 0), 0) });
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Width < 0 || rectangleSize.Height < 0)
            {
                throw new ArgumentException("The rectangle cannot have a negative length side");
            }
            var rect = new Rectangle(0, 0, rectangleSize.Width, rectangleSize.Height);
            var keys = pointForAdd.Keys;
            for (var j=0; j< keys.Count; j++)
            {
                var distanceToCenter = keys[j];
                for(var i=0; i< pointForAdd[distanceToCenter].Count; i++)
                {
                    var pointForPutRectangle = pointForAdd[distanceToCenter][i];
                    foreach (var corner in GetCornerPositions(rect))
                    {
                        var needRectanlgePosition = new Point(pointForPutRectangle.point.X - corner.X, pointForPutRectangle.point.Y - corner.Y);
                        rect.MoveToPosition(needRectanlgePosition);
                        if (!IntersectsWithPrevious(rect))
                        {
                            AddRectangle(rect);
                            pointForPutRectangle.countNear += 1;
                            if (pointForPutRectangle.countNear == 2)
                            {
                                pointForAdd[distanceToCenter].RemoveAt(i);
                                if (pointForAdd[distanceToCenter].Count == 0)
                                    pointForAdd.RemoveAt(j);
                            }
                            return rect;
                        }
                    }
                }
            }
            return rect;
        }

        private void AddRectangle(Rectangle rect)
        {
            foreach (var middles in GetMiddlesSidesRectangle(rect))
            {
                var point = new Point(rect.X + middles.X, rect.Y + middles.Y);
                var distanceToCenter = point.GetDistance(center);
                if (!pointForAdd.ContainsKey(distanceToCenter))
                    pointForAdd.Add(distanceToCenter, new List<(Point point, int countNear)>());
                pointForAdd[distanceToCenter].Add((point, 0));
            }
            previousRectangles.Add(rect);
        }

        private IEnumerable<Point> GetCornerPositions(Rectangle rectangle)
        {
            yield return new Point(0, 0);
            yield return new Point(rectangle.Width, 0);
            yield return new Point(rectangle.Width, rectangle.Height);
            yield return new Point(0, rectangle.Height);
        }

        private IEnumerable<Point> GetMiddlesSidesRectangle(Rectangle rectangle)
        {
            yield return new Point(rectangle.Width / 2, 0);
            yield return new Point(rectangle.Width, rectangle.Height / 2);
            yield return new Point(rectangle.Width / 2, rectangle.Height);
            yield return new Point(0, rectangle.Height / 2);
        }

        private bool IntersectsWithPrevious(Rectangle rectangle)
        {
            return previousRectangles.Any(previousRectangle => previousRectangle.IntersectsWith(rectangle));
        }
    }
}
