using System.Drawing;

namespace TagCloudGenerator.Tests
{
    public static class TestsHelper
    {
        public static (Rectangle, Rectangle)? GetAnyPairOfIntersectingRectangles(Rectangle[] rectangles)
        {
            for (int i = 0; i < rectangles.Length; i++)
                for (int j = i + 1; j < rectangles.Length; j++)
                    if (rectangles[i].IntersectsWith(rectangles[j]))
                        return (rectangles[i], rectangles[j]);
            return null;
        }
    }
}