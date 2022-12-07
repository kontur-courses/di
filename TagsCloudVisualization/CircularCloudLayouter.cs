using System.Drawing;

namespace TagsCloudVisualization;

public class CircularCloudLayouter
{
    private readonly double _spiralStep;
    private readonly ArithmeticSpiral _arithmeticSpiral;
    private readonly List<Rectangle> _rectangles;

    public IEnumerable<Rectangle> Rectangles => _rectangles;
    public Point Center => _arithmeticSpiral.Center;

    public CircularCloudLayouter(Point center, double spiralStep = 1)
    {
        if (spiralStep <= 0)
            throw new ArgumentException("Zero or negative spiral step are not allowed");

        _arithmeticSpiral = new ArithmeticSpiral(center);
        _spiralStep = spiralStep;
        _rectangles = new List<Rectangle>();
    }

    public Rectangle PutNextRectangle(Size rectangleSize)
    {
        if (rectangleSize.IsEmpty)
            return new Rectangle(0, 0, 0, 0);

        if (rectangleSize.Width < 1 || rectangleSize.Height < 1)
            return new Rectangle(0, 0, 0, 0);

        var currentLength = 0d;
        Rectangle rectangle;

        do
        {
            var nextPoint = _arithmeticSpiral.GetPoint(currentLength);
            var centeredPoint = ShiftPointToRectangleCenter(nextPoint, rectangleSize);
            rectangle = new Rectangle(centeredPoint, rectangleSize);
            currentLength += _spiralStep;
        } while (_rectangles.Any(rect => rect.IntersectsWith(rectangle)));

        _rectangles.Add(rectangle);
        return rectangle;
    }

    private Point ShiftPointToRectangleCenter(Point sourcePoint, Size rectSize)
    {
        var newX = sourcePoint.X - rectSize.Width / 2;
        var newY = sourcePoint.Y - rectSize.Height / 2;

        return new Point(newX, newY);
    }
}