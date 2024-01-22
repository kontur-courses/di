using SixLabors.ImageSharp;
using System.Numerics;

namespace TagsCloudVisualization;

public class Layout : ILayout
{
    private readonly PointF center;
    private readonly ILayoutFunction layoutFunction;
    private readonly IList<RectangleF> placedRectangles;

    public Layout(ILayoutFunction layoutFunction, PointF center)
    {
        this.center = center;
        this.layoutFunction = layoutFunction;
        placedRectangles = new List<RectangleF>();
    }

    public int RectangleCount => placedRectangles.Count;

    public RectangleF PutNextRectangle(SizeF rectSize)
    {
        var rectangle = GetCorrectlyPlacedRectangle(rectSize);
        var movedRectangle = GetMovedToCenterRectangle(rectangle);
        placedRectangles.Add(movedRectangle);

        return movedRectangle;
    }

    private RectangleF GetMovedToCenterRectangle(RectangleF rectangle)
    {
        var currentRect = rectangle;

        // Skip 1-st rectangle, because it's already in (0, 0) point.
        if (placedRectangles.Count == 0)
            return currentRect;

        var toCenter = new Vector2(center.X - currentRect.X, center.Y - currentRect.Y);
        var length = (float)Math.Sqrt(toCenter.X * toCenter.X + toCenter.Y * toCenter.Y);
        var normalized = new Vector2(toCenter.X / length, toCenter.Y / length);

        while (true)
        {
            var point = new PointF(currentRect.X + normalized.X, currentRect.Y + normalized.Y);
            var newRect = new RectangleF(point, currentRect.Size);

            if (placedRectangles.Any(rect => rect.IntersectsWith(newRect)))
                break;

            currentRect = newRect;
        }

        return currentRect;
    }

    private RectangleF GetCorrectlyPlacedRectangle(SizeF rectSize)
    {
        while (true)
        {
            var tempPoint = layoutFunction.GetNextPoint().Center(center);

            var common = new RectangleF(tempPoint, rectSize);
            var rotated = common with { Width = common.Height, Height = common.Width };

            var commonPoint = tempPoint.ApplyOffset(-common.Width / 2, -common.Height / 2);
            var rotatedPoint = tempPoint.ApplyOffset(-rotated.Width / 2, -rotated.Height / 2);

            (common.X, common.Y) = (commonPoint.X, commonPoint.Y);
            (rotated.X, rotated.Y) = (rotatedPoint.X, rotatedPoint.Y);

            if (!Intersects(common))
                return common;

            if (Math.Abs(rotated.Width - rotated.Height) < 1e-3 && !Intersects(rotated))
                return rotated;
        }
    }

    private bool Intersects(RectangleF rectangle)
    {
        for (var i = placedRectangles.Count - 1; i > -1; i--)
            if (rectangle.IntersectsWith(placedRectangles[i]))
                return true;

        return false;
    }
}