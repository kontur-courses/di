using System;

namespace TagsCloudVisualization.Tests
{
    public static class SquareCalculator
    {
        public static double CalculateCircleSquare(double radius)
        {
            return Math.PI * radius * radius;
        }

        public static double CalculateRectangleSquare(double width, double height)
        {
            return width * height;
        }
    }
}