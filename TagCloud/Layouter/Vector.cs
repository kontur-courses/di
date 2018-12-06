using System;

namespace TagCloud.Layouter
{
    public class Vector
    {
        public Vector(Point position)
        {
            Position = position;
        }

        public Vector(Point startPoint, Point endPoint)
        {
            Position = endPoint - startPoint;
        }

        public Point Position { get; }

        public Vector Normalized()
        {
            return new Vector(Position.Normalized());
        }

        public double GetLength()
        {
            return Math.Sqrt(Math.Pow(Position.X, 2) + Math.Pow(Position.Y, 2));
        }

        public static double GetLength(Point start, Point end)
        {
            return new Vector(start, end).GetLength();
        }

        public double ScalarMultiply(Vector otherVector)
        {
            var x = Position.X * otherVector.Position.X;
            var y = Position.Y * otherVector.Position.Y;

            return x + y;
        }

        public bool IsSameDirection(Vector otherVector)
        {
            if (this == otherVector)
                return true;
            return ScalarMultiply(otherVector) > 0;
        }
    }
}