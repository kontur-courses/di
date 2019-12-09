using System.Drawing;

namespace CircularCloudLayouter
{
    public static class RectangleExtension
    {
        public static Point GetRectangleCenter(this Rectangle rect)
        {
            return new Point(rect.Left + rect.Width / 2,
                     rect.Top + rect.Height / 2);
        }

        public static bool IsIntersected(this Rectangle rect, Segment segment)
        {
            if (segment.Horizontal())
            {
                if (segment.Start.X < rect.Right
                && segment.Start.X >= rect.Left
                && segment.Start.Y > rect.Top
                && segment.Start.Y < rect.Bottom)
                    return true;
                if (segment.End.X <= rect.Right
                && segment.End.X > rect.Left
                && segment.End.Y > rect.Top
                && segment.End.Y < rect.Bottom)
                    return true;
                if (rect.Top < segment.Start.Y
                    && rect.Bottom > segment.Start.Y
                    && rect.Left > segment.Start.X
                    && rect.Right < segment.End.X)
                    return true;
            }
            if (!segment.Horizontal())
            {
                if (segment.Start.X < rect.Right
                    && segment.Start.X > rect.Left
                    && segment.Start.Y >= rect.Top
                    && segment.Start.Y < rect.Bottom)
                    return true;
                if (segment.End.X < rect.Right
                    && segment.End.X > rect.Left
                    && segment.End.Y > rect.Top
                    && segment.End.Y <= rect.Bottom)
                    return true;
                if (rect.Left < segment.Start.X
                    && rect.Right > segment.Start.X
                    && rect.Top > segment.Start.Y
                    && rect.Bottom < segment.End.Y)
                    return true;
            }
            return false;
        }
    }
}
