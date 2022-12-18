using System.Drawing;
using TagCloudPainter.Extensions;

namespace TagCloudPainter.Layouters;

public class CircularCloudLayouter : ICloudLayouter
{
    private readonly Point center;
    private readonly HelixPointLayouter helixPointLayouter;
    private readonly List<Rectangle> rectangles;

    public CircularCloudLayouter(Point center, double angleStep, double radiusStep)
    {
        this.center = center;
        helixPointLayouter = new HelixPointLayouter(center, angleStep, radiusStep);
        rectangles = new List<Rectangle>();
    }

    public List<Rectangle> Rectangles => rectangles.ToList();

    public Rectangle PutNextRectangle(Size rectangleSize)
    {
        if (rectangleSize.Width == 0 || rectangleSize.Height == 0)
            throw new ArgumentException();

        var location = helixPointLayouter.GetPoint();
        var rectangle = new Rectangle(location, rectangleSize);
        while (rectangle.IsIntersectsOthersRectangles(rectangles))
        {
            location = helixPointLayouter.GetPoint();
            rectangle = new Rectangle(location, rectangleSize);
        }

        var movingPoint = GetMovingToPointVector(rectangle, center);

        ShiftRectangleToCenter(ref rectangle, movingPoint);

        rectangles.Add(rectangle);
        return rectangle;
    }

    private void ShiftRectangleToCenter(ref Rectangle rectangle, Point shiftPoint)
    {
        if (shiftPoint.X == 0 && shiftPoint.Y == 0)
            return;

        var resultRectangle = rectangle;
        var stepX = shiftPoint with { Y = 0 };
        var stepY = shiftPoint with { X = 0 };
        while ((CanMoveRectangle(resultRectangle, stepX) || CanMoveRectangle(resultRectangle, stepY)) &&
               resultRectangle.X != center.X && resultRectangle.Y != center.Y)
        {
            if (CanMoveRectangle(resultRectangle, stepX))
                resultRectangle = GetMovedRectangle(resultRectangle, stepX);
            if (CanMoveRectangle(resultRectangle, stepY))
                resultRectangle = GetMovedRectangle(resultRectangle, stepY);
        }

        rectangle = resultRectangle;
    }

    private bool CanMoveRectangle(Rectangle rectangle, Point movePoint)
    {
        var r = GetMovedRectangle(rectangle, movePoint);
        return !r.IsIntersectsOthersRectangles(rectangles);
    }

    private Rectangle GetMovedRectangle(Rectangle rectangle, Point point)
    {
        return new Rectangle(new Point(rectangle.X + point.X, rectangle.Y + point.Y), rectangle.Size);
    }

    private Point GetMovingToPointVector(Rectangle rectangle, Point point)
    {
        var x = point.X - rectangle.X == 0 ? 0 : (point.X - rectangle.X) / Math.Abs(point.X - rectangle.X);
        var y = point.Y - rectangle.Y == 0 ? 0 : (point.Y - rectangle.Y) / Math.Abs(point.Y - rectangle.Y);
        return new Point(x, y);
    }
}