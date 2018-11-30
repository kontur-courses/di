using System;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class ArchimedesSpiral: ISpiral
    {
        public ArchimedesSpiral(Point center)
        {
            Center = center;
        }

        public Point Center { get; }
        private double angle = 0;

        private const double SpiralShift = 0.5;
        private const double AngleShift = 0.01;

        private Point GetCurrentPositionOnTheSpiral()
        {
            var x = Center.X + (SpiralShift * angle * Math.Cos(angle));
            var y = Center.Y + (SpiralShift * angle * Math.Sin(angle));

            return new Point((int)x, (int)y);
        }

        public Rectangle GetRectangleInNextLocation(Size rectangleSize)
        {
            angle += AngleShift;
            var rectangle = new Rectangle(GetCurrentPositionOnTheSpiral(), rectangleSize);

            return rectangle.ShiftCoordinatesToCenterRectangle();
        }
    }
}
