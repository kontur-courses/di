using System;

namespace TagsCloudVisualization
{
    public class Vector
    {
        public Point Position { get; }

        public Vector(Point position)
            => Position = position;

        public Vector(Point startPoint, Point endPoint)
            => Position = endPoint - startPoint;

        public Vector Normalized()
            => new Vector(Position.Normalized());

        public double GetLength()
            => Math.Sqrt(Math.Pow(Position.X, 2) + Math.Pow(Position.Y, 2));

        public static double GetLength(Point start, Point end)
            => new Vector(start, end).GetLength();

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
