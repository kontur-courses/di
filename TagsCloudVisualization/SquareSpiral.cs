using System.Drawing;

namespace TagsCloudVisualization
{
    public class SquareSpiral : ISpiral
    {
        public SquareSpiral(Point center)
        {
            Center = center;
        }

        public Point Center { get; }

        private static int l = 10;
        private static int t = 10;

        private static int y0 = t;
        private static int y1 = y0;
        private static int y2 = y0 + l + t;
        private static int y3 = y0 + l + t;

        private static int x0 = l;
        private static int x1 = x0 + l;
        private static int x2 = x0 + l;
        private static int x3 = x0 - l;

        private const double SpiralShift = 1;
        private const double AngleShift = 0.05;

        private int num = 0;

        private Point GetCurrentPositionOnTheSpiral()
        {
            num++;
            var point = Center;
            switch (num - num % 4)
            {
                case 0:
                    point.Offset(new Point(x0 - t * num, y0 - t * num));
                    break;
                case 1:
                    point.Offset(new Point(x1 + t * num, y1 - t * num));
                    break;
                case 2:
                    point.Offset(new Point(x2 + t * num, y2 + t * num));
                    break;
                case 3:
                    point.Offset(new Point(x3 - t * num, y3 + t * num));
                    break;
            }

            return point;
        }

        public Rectangle GetRectangleInNextLocation(Size rectangleSize)
        {
            var rectangle = new Rectangle(GetCurrentPositionOnTheSpiral(), rectangleSize);

            return rectangle.ShiftCoordinatesToCenterRectangle();
        }
    }
}