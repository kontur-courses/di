using System.Drawing;

namespace TagsCloudVisualization;

public class ArchimedeanSpiral : ICurve
{
    public readonly double _density;
    private readonly int _maxRadiusSize = int.MaxValue / 2;
    private readonly double _start;
    private readonly double _step;

    public ArchimedeanSpiral(double step, double density, double start)
    {
        if (step == 0 || density == 0)
            throw new ArgumentException("Step and Density must be non-zero!");
        if (start + density * step > _maxRadiusSize)
            throw new ArgumentException(
                "Too much value! Value of [start + density * step] must be less than int.MaxValue!");

        _step = step;
        _density = density;
        _start = start;
    }

    public IEnumerable<Point> GetNextPoint()
    {
        var curRadius = 0.0;
        var curAngle = 0.0;

        while (curRadius < _maxRadiusSize)
        {
            var polarPoint = new PolarPoint(curRadius, curAngle);
            yield return (Point)polarPoint;
            curAngle += _step;
            curRadius = _start + _density * curAngle;
        }
    }
}