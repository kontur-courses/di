using SixLabors.ImageSharp;

namespace TagsCloudVisualization;

public static class PointFExtensions
{
    public static PointF ConvertToCartesian(this PointF point)
    {
        var (radius, angle) = point;

        var x = radius * (float)Math.Cos(angle);
        var y = radius * (float)Math.Sin(angle);

        (point.X, point.Y) = (x, y);

        return point;
    }

    public static PointF Center(this PointF point, PointF center)
    {
        point.X += center.X;
        point.Y += center.Y;

        return point;
    }

    public static PointF ApplyOffset(this PointF point, float offsetX, float offsetY)
    {
        point.X += offsetX;
        point.Y += offsetY;

        return point;
    }
}