using System;
using System.Drawing;

namespace TagsCloudVisualization.Tests.Utils
{
    public static class SquareCalculator
    {
        public static double CalculateCircleSquare(double radius) => Math.PI * radius * radius;

        public static double CalculateRectangleSquare(double width, double height) => width * height;

        public static double CalculateRectangleSquare(Size size) => CalculateRectangleSquare(size.Width, size.Height);
    }
}