using System;
using System.Drawing;

namespace TagCloud.Algorithm.SpiralBasedLayouter
{
    public class Segment
    {
        public Point Right;
        public Point Left;

        public Segment(int left, int right)
        {
            Left = new Point(left, 0);
            Right = new Point(right, 0);
        }

        public Segment(Point left, Point right)
        {
            Left = left;
            Right = right;
        }

        public PointF GetCenter()
        {
            var x = (float)(Left.X + Right.X) / 2;
            var y = (float)(Left.Y + Right.Y) / 2;
            return new PointF(x, y);
        }

        public static bool SegmentsAreIntersected(Segment firstSegment, Segment secondSegment)
        {
            return Math.Max(firstSegment.Left.X, secondSegment.Left.X) <
                   Math.Min(firstSegment.Right.X, secondSegment.Right.X);
        }
    }
}
