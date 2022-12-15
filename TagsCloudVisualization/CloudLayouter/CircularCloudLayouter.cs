using System.Drawing;
using System.Numerics;

namespace TagsCloudVisualization.CloudLayouter;

public class CircularCloudLayouter : ICloudLayouter
{
    private readonly ISpiralFormula _arithmeticSpiral;
    private readonly List<RectangleF> _rectangles;

    public IEnumerable<RectangleF> Rectangles => _rectangles;

    public CircularCloudLayouter(ISpiralFormula arithmeticSpiral)
    {
        _arithmeticSpiral = arithmeticSpiral;
        _rectangles = new List<RectangleF>();
    }

    public RectangleF PutNextRectangle(SizeF rectangleSize, LayoutOptions options)
    {
        if (options.SpiralStep <= 0)
            throw new ArgumentException("SpiralStep must be greater than 0");

        if (rectangleSize.IsEmpty)
            return new RectangleF(0, 0, 0, 0);

        if (rectangleSize.Width < 1 || rectangleSize.Height < 1)
            return new RectangleF(0, 0, 0, 0);

        var currentLength = 0f;
        RectangleF rectangle;

        do
        {
            var nextPoint = _arithmeticSpiral.GetPoint(options.Center, currentLength);
            var centeredPoint = ShiftPointToRectangleCenter(nextPoint, rectangleSize);
            rectangle = new RectangleF(centeredPoint, rectangleSize);
            currentLength += options.SpiralStep;
        } while (_rectangles.Any(rect => rect.IntersectsWith(rectangle)));

        _rectangles.Add(rectangle);
        return rectangle;
    }

    private PointF RotatePoint(PointF point, SizeF rectSize, float angle)
    {
        angle = (float)(angle * Math.PI / 180.0);
        var x = Math.Cos(angle) * (point.X - point.X + rectSize.Width / 2) - Math.Sin(angle) * (point.Y - point.Y + rectSize.Height / 2) + point.X + rectSize.Width;
        var y = Math.Sin(angle) * (point.X - point.X + rectSize.Width / 2) + Math.Cos(angle) * (point.Y - point.Y + rectSize.Height / 2) + point.Y + rectSize.Height / 2;

        return new PointF((float)x, (float)y);
    }

    private static PointF ShiftPointToRectangleCenter(PointF sourcePoint, SizeF rectSize)
    {
        var newX = sourcePoint.X - rectSize.Width / 2;
        var newY = sourcePoint.Y - rectSize.Height / 2;

        return new PointF(newX, newY);
    }

    public void Reset()
    {
        _rectangles.Clear();
    }
}