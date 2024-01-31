using System.Drawing;

namespace TagsCloudContainer.utility;

public static class PointMath
{
    public static Point PolarToCartesian(int radius, int angle, int xOffset = 0, int yOffset = 0)
    {
        return new Point(
            (int)(radius * Math.Cos(angle * Math.PI / 180.0)) + xOffset,
            (int)(radius * Math.Sin(angle * Math.PI / 180.0)) + yOffset
        );
    }
}