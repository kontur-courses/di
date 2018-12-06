using System;
using System.Drawing;

namespace TagsCloudVisualization_Tests
{
    public static class Helper
    {
        public static int MaxSignedAbs(int val1, int val2) =>
            Math.Abs(val1) == Math.Max(Math.Abs(val1), Math.Abs(val2)) ? val1 : val2;
        
        public static int GetDistanceTo(this Point point1, Point point2) =>
            (int) Math.Sqrt(Math.Pow(point1.X - point2.X, 2) + Math.Pow(point1.Y - point2.Y, 2));
        
    }
}
