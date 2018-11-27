using System;
using System.Drawing;

namespace TagsCloudVisualization
{
    class ArchimedesSpiral: ISpiral
    {
        public ArchimedesSpiral(Point center)
        {
            this.center = center;
        }

        private readonly Point center;
        private double angle = 0;

        private const double SpiralShift = 1;
        private const double AngleShift = 0.05;

        private Point GetCurrentPositionOnTheSpiral()
        {
            var x = center.X + (SpiralShift * angle * Math.Cos(angle));
            var y = center.Y + (SpiralShift * angle * Math.Sin(angle));

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
