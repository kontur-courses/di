using System.Drawing;
using CloudLayout.Interfaces;

namespace CloudLayout
{
    public class CircularCloudLayout : ILayout
    {
        public readonly int radius;
        private readonly List<PointF> spiralPoints;
        private List<RectangleF> placedRectangles;

        public CircularCloudLayout(ISpiralDrawer drawer, IInputOptions options)
        {
            var center = options.CenterPoint;
            if (center.X < 1)
                throw new ArgumentException("X should be positive number");
            if (center.Y < 1)
                throw new ArgumentException("Y should be positive number");
            radius = center.X < center.Y ? center.X : center.Y;
            placedRectangles = new();
            spiralPoints = drawer.GetSpiralPoints(center);
        }

        public bool PutNextRectangle(SizeF size, out RectangleF rectangle)
        {
            rectangle = new RectangleF();
            if (!ValidateSize(size))
                throw new ArgumentException("Both dimensions must be above zero");
            if (placedRectangles.Count == 0)
                return TryPlaceRectangleInCenter(out rectangle, size);
            foreach (var point in spiralPoints)
            {
                if (PointLiesInRectangles(point))
                    continue;
                rectangle = TryPlaceRectangle(point, size);
                if (rectangle.IsEmpty)
                    continue;
                OffsetRectangle(ref rectangle);
                break;
            }

            if (rectangle.IsEmpty)
                return false;
            placedRectangles.Add(rectangle);
            return true;
        }

        private void OffsetRectangle(ref RectangleF rectangle)
        {
            var canOffsetX = true;
            var canOffsetY = true;
            while (canOffsetY || canOffsetX)
            {
                canOffsetX = rectangle.GetCenter().X > spiralPoints[0].X
                    ? TryOffSet(ref rectangle, -1, 0, (rectangle) => rectangle.GetCenter().X < spiralPoints[0].X)
                    : TryOffSet(ref rectangle, 1, 0, (rectangle) => rectangle.GetCenter().X > spiralPoints[0].X);
                canOffsetY = rectangle.GetCenter().Y > spiralPoints[0].Y
                    ? TryOffSet(ref rectangle, 0, -1, (rectangle) => rectangle.GetCenter().Y < spiralPoints[0].Y)
                    : TryOffSet(ref rectangle, 0, 1, (rectangle) => rectangle.GetCenter().Y > spiralPoints[0].Y);
            }
        }

        private bool TryOffSet(ref RectangleF rectangle, int x, int y, Func<RectangleF, bool> closeToCenter)
        {
            var buffer = rectangle;
            buffer.Offset(x, y);
            if (closeToCenter(buffer))
                return false;
            if (RectangleIntersects(buffer))
                return false;
            rectangle = buffer;
            return true;
        }

        private RectangleF AdjustRectanglePosition(PointF point, SizeF size)
        {
            var x = point.X > spiralPoints[0].X ? point.X : point.X - size.Width;
            var y = point.Y > spiralPoints[0].Y ? point.Y : point.Y - size.Height;
            return new RectangleF(new PointF(x, y), size);
        }

        private RectangleF TryPlaceRectangle(PointF pointer, SizeF size)
        {
            var rectangle = AdjustRectanglePosition(pointer, size);
            if (!RectangleIntersects(rectangle) && !RectangleOutOfCircleRange(rectangle))
                return rectangle;
            return new RectangleF();
        }

        private bool TryPlaceRectangleInCenter(out RectangleF rectangle, SizeF size)
        {
            rectangle = new RectangleF(
                new PointF(spiralPoints[0].X - size.Width / 2, spiralPoints[0].Y - size.Height / 2),
                size);
            if (RectangleOutOfCircleRange(rectangle))
                return false;
            placedRectangles.Add(rectangle);
            return true;
        }

        private bool PointLiesInRectangles(PointF p) => placedRectangles.Any(x => x.Contains(p));

        private bool RectangleIntersects(RectangleF rectangle) =>
            placedRectangles.Any(x => x.IntersectsWith(rectangle));

        private bool RectangleOutOfCircleRange(RectangleF rectangle)
        {
            var x1 = rectangle.Left - spiralPoints[0].X;
            var y1 = rectangle.Top - spiralPoints[0].Y;
            var x2 = rectangle.Right - spiralPoints[0].X;
            var y2 = rectangle.Bottom - spiralPoints[0].Y;
            return Math.Sqrt(x1 * x1 + y1 * y1) > radius
                   || Math.Sqrt(x2 * x2 + y1 * y1) > radius
                   || Math.Sqrt(x1 * x1 + y2 * y2) > radius
                   || Math.Sqrt(x2 * x2 + y2 * y2) > radius;
        }

        private static bool ValidateSize(SizeF size) => size.Height > 0 && size.Width > 0;
    }
}