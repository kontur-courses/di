using System;
using System.Drawing;

namespace TagsCloudVisualization
{
    public static class ExtensionMethods
    {
        public static double CalculateDistanceBetweenTwoPoints(this Point point1, Point point2)
        {
            return Math.Sqrt((point1.X - point2.X) * (point1.X - point2.X)
                             + (point1.Y - point2.Y) * (point1.Y - point2.Y));
        }

        public static bool IsBelowAnother(this Rectangle rectangle1, Rectangle rectangle2)
        {
            return rectangle1.Y >= rectangle2.Y + rectangle2.Height;
        }

        public static bool IsAboveAnother(this Rectangle rectangle1, Rectangle rectangle2)
        {
            return rectangle1.Y + rectangle1.Height <= rectangle2.Y;
        }

        public static bool ToRightOfAnother(this Rectangle rectangle1, Rectangle rectangle2)
        {
            return rectangle1.X >= rectangle2.X + rectangle2.Width;
        }

        public static bool ToLeftOfAnother(this Rectangle rectangle1, Rectangle rectangle2)
        {
            return rectangle1.X + rectangle1.Width <= rectangle2.X;
        }
    }
}