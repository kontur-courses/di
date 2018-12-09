namespace TagCloud.Layouter
{
    public class Point
    {
        public Point()
        {
        }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; }
        public double Y { get; }

        public static Point operator +(Point p1, Point p2)
        {
            var x = p1.X + p2.X;
            var y = p1.Y + p2.Y;

            return new Point(x, y);
        }

        public static Point operator +(Point point, Vector vector)
        {
            var x = point.X + vector.Position.X;
            var y = point.Y + vector.Position.Y;

            return new Point(x, y);
        }

        public static Point operator -(Point p1, Point p2)
        {
            var x = p1.X - p2.X;
            var y = p1.Y - p2.Y;

            return new Point(x, y);
        }

        public Point Normalized()
        {
            return new Point(1 / X, 1 / Y);
        }


        public override string ToString()
        {
            return $"Point (X: {X}; Y: {Y})";
        }
    }
}