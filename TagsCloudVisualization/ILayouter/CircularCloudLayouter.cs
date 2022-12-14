using System.Drawing;

namespace TagsCloudVisualization;

//Позиции для прямоугольников выбираем по спирали, которая задаётся формулой: r = a + (b * angle)
public class CircularCloudLayouter : ILayouter
{
    private readonly Point _center;
    private readonly ICurve _curve;
    private readonly List<Rectangle> _rectangles = new();

    public CircularCloudLayouter(ICurve curve)
    {
        _center = new Point(0, 0);
        _curve = curve;
    }

    public Rectangle PutNextRectangle(Size rectangleSize)
    {
        if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
            throw new ArgumentException("Width and Height must be positive!");

        foreach (var rectangleCenter in _curve.GetNextPoint())
        {
            var applicantLocation = CalculateRectanglePosition(rectangleCenter, rectangleSize);
            var applicantRectangle = new Rectangle(applicantLocation, rectangleSize);
            if (!applicantRectangle.CheckForIntersectionWithRectangles(_rectangles))
            {
                _rectangles.Add(applicantRectangle);
                return applicantRectangle;
            }
        }

        throw new ArgumentException("Rectangle doesnt fit in circle");
    }

    private Point CalculateRectanglePosition(Point rectangleCenter, Size rectangleSize)
    {
        var X = _center.X + rectangleCenter.X - rectangleSize.Width / 2;
        var Y = _center.Y + rectangleCenter.Y - rectangleSize.Height / 2;

        return new Point(X, Y);
    }
}