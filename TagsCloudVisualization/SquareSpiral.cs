using System;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class SquareSpiral : ISpiral
    {
        public SquareSpiral(Point center)
        {
            this.Center = center;
        }

        public Point Center { get; }
        private double angle = 0;

        private const double SpiralShift = 1;
        private const double AngleShift = 0.05;

        private Point GetCurrentPositionOnTheSpiral()
        {
            throw new NotImplementedException();
        }

        public Rectangle GetRectangleInNextLocation(Size rectangleSize)
        {
            angle += AngleShift;
            var rectangle = new Rectangle(GetCurrentPositionOnTheSpiral(), rectangleSize);

            return rectangle.ShiftCoordinatesToCenterRectangle();
        }
    }
}
