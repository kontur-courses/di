using System.Drawing;

namespace TagsCloudContainer;

public static class CircularHelper
{
    public static IEnumerable<Point> EnumeratePointsInArchimedesSpiral(float polarStepK,
        float angleStep,
        Point center, float startAngle = 0f)
    {
        var current = new PointF(center.X, center.Y);
        var centerX = (float)center.X;
        var centerY = (float)center.Y;
        var angle = startAngle;

        while (true)
        {
            yield return Point.Round(current);

            var p = polarStepK * angle;
            var x = centerX + p * MathF.Cos(angle);
            var y = centerY + p * MathF.Sin(angle);

            current = new(x, y);
            angle += angleStep;
        }
    }
}