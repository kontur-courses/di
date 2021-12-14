using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MoreLinq.Extensions;
using TagsCloudVisualization.Common.Settings;
using TagsCloudVisualization.Extensions;

namespace TagsCloudVisualization.Common.Layouters
{
    public class CircularCloudLayouter : ILayouter
    {
        private readonly List<Rectangle> rects;

        public Point LayoutCenter { get; }
        public IEnumerable<Rectangle> Rects => rects.AsReadOnly();


        public CircularCloudLayouter(ICanvasSettings settings)
        {
            rects = new List<Rectangle>();
            LayoutCenter = new Point(settings.Width / 2, settings.Height / 2);
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
                throw new ArgumentException("Размеры прямоугольника должны быть больше 0");

            var newRect = rects.Any()
                ? rects.SelectMany(rect => GetPossibleRectangles(rect, rectangleSize))
                    .MinBy(possibleRect => possibleRect.Distance).First().Rect
                : new Rectangle(
                    new Point(LayoutCenter.X - rectangleSize.Width / 2, LayoutCenter.Y - rectangleSize.Height / 2),
                    rectangleSize);

            rects.Add(newRect);
            return newRect;
        }

        private IEnumerable<(Rectangle Rect, double Distance)> GetPossibleRectangles(Rectangle rect, Size newRectSize)
        {
            (Point, Point)[] waypoints =
            {
                (new Point(rect.Left - newRectSize.Width, rect.Top - newRectSize.Height),
                    new Point(rect.Right, rect.Top - newRectSize.Height)),
                (new Point(rect.Right, rect.Top - newRectSize.Height),
                    new Point(rect.Right, rect.Bottom)),
                (new Point(rect.Left - newRectSize.Width, rect.Bottom),
                    new Point(rect.Right, rect.Bottom)),
                (new Point(rect.Left - newRectSize.Width, rect.Top - newRectSize.Height),
                    new Point(rect.Left - newRectSize.Width, rect.Bottom))
            };

            foreach (var (start, stop) in waypoints)
            {
                for (var x = start.X; x <= stop.X; x++)
                {
                    for (var y = start.Y; y <= stop.Y; y++)
                    {
                        var newRect = new Rectangle(new Point(x, y), newRectSize);
                        var distance = newRect.GetCenter().GetDistance(LayoutCenter);
                        if (!rects.Any(r => r.IntersectsWith(newRect)))
                            yield return (newRect, distance);
                    }
                }
            }
        }
    }
}