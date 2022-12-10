using System.Drawing;

namespace TagCloud.CloudLayouter
{
    public class Vector
    {
        public int X { get; set; }

        public int Y { get; set; }

        public Vector(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Vector(Point startPoint, Point endPoint)
        {
            X = endPoint.X - startPoint.X;
            Y = endPoint.Y - startPoint.Y;
        }

        public bool IsPerpendicularTo(Vector other)
        {
            return X * other.X == 0 && Y * other.Y == 0;
        }
    }
}