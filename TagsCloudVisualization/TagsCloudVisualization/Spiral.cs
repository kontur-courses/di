using System.Drawing;

namespace TagsCloudVisualization;

class Spiral
{
    private float _alpha;
    private float _phi0;
    private Point _center;

    private float _phi;

    private float R
    {
        get => _alpha * (_phi - _phi0);
    }

    public Spiral(float alpha, Point center, float phi0 = 0)
    {
        _alpha = alpha;
        _phi0 = phi0;
        _center = center;
    }

    public void SetNewStartAngle(float phi0)
    {
        _phi0 = phi0;
        _phi = phi0;
    }

    public void SetNewRandomStartAngle() =>
        SetNewStartAngle(new Random().NextSingle() * (float)Math.PI * 2);

    public void IncreaseAngle(float angle) =>
        _phi += angle;

    public Point ToCartesian() =>
        new Point(
            (int)(R * Math.Cos(_phi)) + _center.X,
            (int)(R * Math.Sin(_phi) + _center.Y)
        );
}