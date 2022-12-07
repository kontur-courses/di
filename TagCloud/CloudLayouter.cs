using System.Drawing;
using TagCloud.Curves;
using TagCloud.Extensions;

namespace TagCloud;

public class CloudLayouter
{
    private readonly ICurve _curve;
    private readonly double _curveStep;
    private readonly List<Rectangle> _rectangles = new();
    private double _lastCurveParameter;

    public CloudLayouter(ICurve curve, Point center, double curveStep = 0.01)
    {
        Center = center;
        _curve = curve;
        _curveStep = curveStep;
    }

    public CloudLayouter(ICurve curve) : this(curve, Point.Empty)
    {
    }

    public IEnumerable<Rectangle> Rectangles => _rectangles;
    public Point Center { get; }

    public Rectangle PutRectangle(Size rectangleSize)
    {
        if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
            throw new ArgumentException("Rectangles' width and height should be positive.");
        var rectangle = new Rectangle(Point.Empty, rectangleSize);
        rectangle = PlaceRectangle(rectangle);
        rectangle = ShiftRectangleToCenter(rectangle);
        _rectangles.Add(rectangle);
        return rectangle;
    }

    private Rectangle PlaceRectangle(Rectangle rectangle)
    {
        do
        {
            rectangle.Location = _curve.GetPoint(_lastCurveParameter) + (Size)Center;
            _lastCurveParameter += _curveStep;
        } while (rectangle.IntersectsWith(_rectangles));

        return rectangle;
    }

    private Rectangle ShiftRectangleToCenter(Rectangle rectangle)
    {
        var dx = rectangle.GetCenter().X < Center.X ? 1 : -1;
        rectangle = ShiftRectangle(rectangle, dx, 0);
        var dy = rectangle.GetCenter().Y < Center.Y ? 1 : -1;
        rectangle = ShiftRectangle(rectangle, 0, dy);
        return rectangle;
    }

    private Rectangle ShiftRectangle(Rectangle rectangle, int dx, int dy)
    {
        var offset = new Size(dx, dy);
        while (rectangle.IntersectsWith(_rectangles) == false &&
               rectangle.GetCenter().X != Center.X &&
               rectangle.GetCenter().Y != Center.Y)
            rectangle.Location += offset;

        if (rectangle.IntersectsWith(_rectangles))
            rectangle.Location -= offset;

        return rectangle;
    }
}