using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Layouters.Spirals;
using PointConverter = TagsCloudVisualization.Utils.PointConverter;

namespace TagsCloudVisualization.Layouters.CloudLayouters
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        private const float Thickness = 1;
        private readonly Point center;
        private readonly List<Rectangle> rectangles;
        private readonly IEnumerator<PointF> spiralPoints;

        public CircularCloudLayouter(Point center)
        {
            this.center = center;
            spiralPoints = new ArchimedesSpiral(center, Thickness).GetSpiralPoints().GetEnumerator();
            rectangles = new List<Rectangle>();
        }

        public CircularCloudLayouter(Point center, ISpiral spiral)
        {
            this.center = center;
            spiralPoints = spiral.GetSpiralPoints().GetEnumerator();
            rectangles = new List<Rectangle>();
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Height <= 0 || rectangleSize.Width <= 0)
                throw new ArgumentException("Size is empty!");
            var rect = new Rectangle(center, rectangleSize);
            while (rectangles.Any(x => x.IntersectsWith(rect)))
            {
                spiralPoints.MoveNext();
                rect.X = (int) spiralPoints.Current.X;
                rect.Y = (int) spiralPoints.Current.Y;
            }

            rect.Location = FindBetterDensityPoint(rect);
            rectangles.Add(rect);
            return rect;
        }

        private Point FindBetterDensityPoint(Rectangle rect)
        {
            if (TryFindPointAtSpiralOneSpinAgo(rect.Location, Thickness, out var previousSpinPointF))
            {
                var previousSpinPoint = new Point((int) previousSpinPointF.X, (int) previousSpinPointF.Y);
                foreach (var point in BuildStraightPath(previousSpinPoint, rect.Location))
                {
                    rect.Location = point;
                    if (!rectangles.Any(x => x.IntersectsWith(rect)))
                        break;
                }
            }

            return rect.Location;
        }

        private bool TryFindPointAtSpiralOneSpinAgo(PointF currentSpinPoint, float a, out PointF previousSpinPoint)
        {
            var (r, theta) =
                PointConverter.TransformCartesianToPolar(currentSpinPoint.X - center.X, currentSpinPoint.Y - center.Y);
            theta -= (float) (2 * Math.PI * a);
            if (theta < 0)
            {
                previousSpinPoint = new PointF(0, 0);
                return false;
            }

            r = theta * a;
            var (x, y) = PointConverter.TransformPolarToCartesian(r, theta);
            previousSpinPoint = new PointF(x + center.X, y + center.Y);
            return true;
        }

        private IEnumerable<Point> BuildStraightPath(Point from, Point to)
        {
            var currentPoint = from;
            while (currentPoint != to)
            {
                if (currentPoint.X != to.X)
                {
                    currentPoint.X -= Math.Sign(currentPoint.X - to.X);
                    yield return currentPoint;
                }

                if (currentPoint.Y != to.Y)
                {
                    currentPoint.Y -= Math.Sign(currentPoint.X - to.Y);
                    yield return currentPoint;
                }
            }
        }
    }
}