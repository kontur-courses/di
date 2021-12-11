using System;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class Vector
    {
        public Point Begin;
        public Point End;

        public Vector(Point begin, Point end)
        {
            Begin = begin;
            End = end;
        }

        public double GetLength()
        {
            var xLen = End.X - Begin.X;
            var yLen = End.Y - Begin.Y;
            return Math.Sqrt(xLen * xLen + yLen * yLen);
        }
    }
}
